using Lollisoft.Common.Logging.Interfaces;
using Lollisoft.Common.Logging.Trace;
using EdwardsIndexAppSecurity;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using Lollisoft.DatabaseLogging.ServiceInterfaces;

namespace ShellCommon
{
    public class LoggingServiceTracer : ILoggingService, IDatabaseTracer
    {
        private readonly WorkItem workItem;

		int _applicationId;
		int _deploymentEnvironmentId;
		int _machineId;

        [InjectionConstructor]
        public LoggingServiceTracer([ServiceDependency] WorkItem workItem)
        {
            this.workItem = workItem;

			int.TryParse(ConfigurationManager.AppSettings.Get("LogApplicationId"), out _applicationId);
			int.TryParse(ConfigurationManager.AppSettings.Get("LogDeploymentEnvironmentId"), out _deploymentEnvironmentId);
			int.TryParse(ConfigurationManager.AppSettings.Get("LogMachineId"), out _machineId);

			// Use this to add trace forwarding to the backend. Otherwise only explicite calls to this instance get forwarded.
            DatabaseTracerFactory.SetTracer(this);
        }

        protected string convertToUNSecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        protected static X509CertificateValidationMode SetCertificateValidationMode(string X509CertificateValidationMode)
        {
            X509CertificateValidationMode mode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;

            switch (X509CertificateValidationMode)
            {
                case "None":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                    break;
                case "PeerTrust":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.PeerTrust;
                    break;
                case "PeerOrChainTrust":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.PeerOrChainTrust;
                    break;
                case "ChainTrust":
                    mode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
                    break;
            }

            return mode;
        }

        private void SetupTokenAuthorization(ChannelFactory<ILoggingService> serviceFactory)
        {
            InMemoryIssuedTokenCache cache = new InMemoryIssuedTokenCache();

            DurableIssuedTokenClientCredentials durableCreds = new DurableIssuedTokenClientCredentials();
            durableCreds.IssuedTokenCache = cache;

            serviceFactory.Endpoint.Behaviors.Remove<ClientCredentials>();
            serviceFactory.Endpoint.Behaviors.Add(durableCreds);

            var credentialBehaviour = serviceFactory.Endpoint.Behaviors.Find<ClientCredentials>();
            string LocalMachine_My_BySubject_ClientCertificate = ConfigurationManager.AppSettings.Get("LocalMachine_My_BySubject_ClientCertificate");
            string LocalMachine_My_BySubject_ServerCertificate = ConfigurationManager.AppSettings.Get("LocalMachine_My_BySubject_ServerCertificate");
            string X509CertificateValidationMode = ConfigurationManager.AppSettings.Get("X509CertificateValidationMode");

            credentialBehaviour.ServiceCertificate.Authentication.CertificateValidationMode = SetCertificateValidationMode(X509CertificateValidationMode);
            if (LocalMachine_My_BySubject_ServerCertificate != null && LocalMachine_My_BySubject_ServerCertificate != "")
                credentialBehaviour.ServiceCertificate.SetDefaultCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, LocalMachine_My_BySubject_ServerCertificate); //"cloud.lollisoft.de");
            if (LocalMachine_My_BySubject_ClientCertificate != null && LocalMachine_My_BySubject_ClientCertificate != "")
                credentialBehaviour.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, LocalMachine_My_BySubject_ClientCertificate); //"cloud.lollisoft.de");

            var cred = workItem.Services.Get<ISecureCredentials>(true);

            string password = convertToUNSecureString(cred.Password);

            serviceFactory.Credentials.UserName.UserName = cred.UserName;
            serviceFactory.Credentials.UserName.Password = password;
        }

        public void LogMessage(LogMessageRequest request)
        {
            using (var serviceFactory = new ChannelFactory<ILoggingService>("LoggingServiceEndpoint"))
            {
                try
                {
                    SetupTokenAuthorization(serviceFactory);

                    ILoggingService profileService = serviceFactory.CreateChannel();

                    var cred = workItem.Services.Get<ISecureCredentials>(true);

                    profileService.LogMessage(request);

                    serviceFactory.Close();
                }
                catch (Exception e)
                {
                    //TODO: Caller must validate result. Here should only be some logs.
                    //XtraMessageBox.Show("Change password failed: " + e.Message, "Error");
                }
                finally
                {
                    // SOA with .NET & Windows Azure - ISBN 0-13-158231-3, page 817
                    // Close may also throw, thus finally cleanup with Abort.
                    if (serviceFactory.State != CommunicationState.Closed)
                    {
                        serviceFactory.Abort();
                    }
                }
            }
        }

        public void TraceMessage(string message)
        {
            TraceMessage(message, TraceEventType.Verbose);
        }

        public void TraceMessage(string message, TraceEventType eventType)
        {
            LogMessage(new LogMessageRequest
            {
                ApplicationId = _applicationId,
                DeploymentEnvironmentId = _deploymentEnvironmentId,
                MachineId = _machineId,
                Messages = new List<LogMessage> 
                {
                    new LogMessage 
                    {
                        ClientTimestamp = DateTime.Now,
                        EventType = eventType,
                        Message = message
                    }
                }
            });
        }
    }
}
