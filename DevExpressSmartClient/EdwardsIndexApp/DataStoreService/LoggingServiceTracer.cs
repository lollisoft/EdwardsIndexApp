using Lollisoft.Common.Logging.Interfaces;
using Lollisoft.Common.Logging.Trace;
using Lollisoft.DatabaseLogging.ServiceInterfaces;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.Claims;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.ServiceModel.Description;
using System.Configuration;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Concurrent;
using System.ServiceModel.Channels;

namespace EdwardsIndexAppDataStoreService
{
    public class LoggingServiceTracer : IDatabaseTracer
	{
        System.Timers.Timer flushTraceBuffer;

        string tokenLock = "";
        SecurityToken token;

        ConcurrentQueue<LogMessage> logBuffer = new ConcurrentQueue<LogMessage>();

		int _applicationId;
		int _deploymentEnvironmentId;
		int _machineId;
        int _logInterval;

        public LoggingServiceTracer()
        {
            flushTraceBuffer = new System.Timers.Timer();

			int.TryParse(ConfigurationManager.AppSettings.Get("LogApplicationId"), out _applicationId);
			int.TryParse(ConfigurationManager.AppSettings.Get("LogDeploymentEnvironmentId"), out _deploymentEnvironmentId);
			int.TryParse(ConfigurationManager.AppSettings.Get("LogMachineId"), out _machineId);
            int.TryParse(ConfigurationManager.AppSettings.Get("LogInterval"), out _logInterval);

            if (_logInterval < 1000) _logInterval = 1000;

            flushTraceBuffer.Interval = _logInterval;

            flushTraceBuffer.Enabled = true;

            flushTraceBuffer.Elapsed += flushTraceBuffer_Elapsed;

            // Use this to add trace forwarding to the backend. Otherwise only explicite calls to this instance get forwarded.
            DatabaseTracerFactory.SetTracer(this);
        }

        public bool FlushMessage(SecurityToken bootstrapToken, LogMessageRequest request)
        {
            string LoggingServiceEndpoint = ConfigurationManager.AppSettings.Get("LoggingServiceEndpoint");

            using (var serviceFactory = new ChannelFactory<ILoggingService>(LoggingServiceEndpoint))
            {
                try
                {
					string LoggingServiceUser = ConfigurationManager.AppSettings.Get("LoggingServiceUser");
					string LoggingServicePass = ConfigurationManager.AppSettings.Get("LoggingServicePass");

					SetupTokenAuthorization(serviceFactory, LoggingServiceUser, LoggingServicePass);

                    //ILoggingService profileService = serviceFactory.CreateChannel();

                    //// http://southworks.com/blog/2008/12/14/managing-the-lifecycle-of-security-tokens-geneva-sts-wcf/
                    //ChannelFactoryOperations.ConfigureChannelFactory(serviceFactory);

                    serviceFactory.ConfigureChannelFactory();
                    serviceFactory.Credentials.SupportInteractive = false;

                    ILoggingService profileService = serviceFactory.CreateChannel();
                    
                    // Both are not working. An Act As capable STS is requiered.
                    // Also for the logging, a technical account is reasonably good.
                    //ILoggingService profileService = serviceFactory.CreateChannelActingAs(new EndpointAddress("net.tcp://cloud.lollisoft.de:8001/stsactingas"), bootstrapToken);
                    //ILoggingService profileService = serviceFactory.CreateChannelWithIssuedToken(bootstrapToken);

                    profileService.LogMessage(request);

                    serviceFactory.Close();

                    return true;
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

                return false;
            }
        }

        void flushTraceBuffer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogMessage logMessage;
            LogMessageRequest logMessageRequest = new LogMessageRequest
            {
                ApplicationId = _applicationId,
                DeploymentEnvironmentId = _deploymentEnvironmentId,
                MachineId = _machineId,
                Messages = new List<LogMessage>()
            };

            while (logBuffer.TryDequeue(out logMessage))
            {
                logMessageRequest.Messages.Add(logMessage);
            }

            lock (tokenLock)
            {
                if (logMessageRequest.Messages.Count > 0)
                {
                    if (!FlushMessage(token, logMessageRequest)) 
                    {
                        foreach (var msg in logMessageRequest.Messages) 
                        {
                            logBuffer.Enqueue(msg);
                        }
                    }
                }
            }
        }
		
        IClaimsIdentity GetClaimsIdentity(OperationContext operationContext)
        {
            ServiceSecurityContext securityContext = operationContext.ServiceSecurityContext;

            var identity = operationContext.ServiceSecurityContext.PrimaryIdentity as IClaimsIdentity;
            if (identity == null)
            {
                // If there is more than 1 identity, for example if there is also a certificate then PrimaryIdentity will be null.
                if (securityContext.AuthorizationContext.Properties.ContainsKey("Principal"))
                {
                    var principal = securityContext.AuthorizationContext.Properties["Principal"] as IClaimsPrincipal;
                    if (principal != null)
                    {
                        identity = principal.Identity as IClaimsIdentity;
                    }
                }

                if (identity == null)
                {
                    //throw new InvalidOperationException("PrimaryIdentity identity is not an IClaimsIdentity");
                }
            }

            return identity;
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

        private void SetupTokenAuthorization(ChannelFactory<ILoggingService> serviceFactory, string username = null, string password = null)
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

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                serviceFactory.Credentials.UserName.UserName = username;
                serviceFactory.Credentials.UserName.Password = password;
            }
        }
		
        public void LogMessage(LogMessage logMessage)
        {
            SecurityToken bootstrapToken = null;

            OperationContext context = OperationContext.Current;

            if (context == null)
                return;

            IClaimsIdentity identity = GetClaimsIdentity(context);
            if (identity != null)
            {
                bootstrapToken = identity.BootstrapToken;
            }

            lock (tokenLock)
            {
                token = bootstrapToken;
            }

            logBuffer.Enqueue(logMessage);
        }

        public void TraceMessage(string message)
        {
            TraceMessage(message, TraceEventType.Verbose);
        }

        public void TraceMessage(string message, TraceEventType eventType)
        {
			string address = "Undefined";
			
            OperationContext context = OperationContext.Current;
            
			if (context != null)
			{
				MessageProperties properties = context.IncomingMessageProperties;
				RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
				address = endpoint.Address;
			}
			
            LogMessage(new LogMessage
            {
				ClientTimestamp = DateTime.Now,
                EventType = eventType,
                Message = message,
                IPAddress = address
            });
        }
    }
}
