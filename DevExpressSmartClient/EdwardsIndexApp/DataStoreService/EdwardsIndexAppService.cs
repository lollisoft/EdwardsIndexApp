
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace EdwardsIndexAppDataStoreService
{
    public partial class EdwardsIndexAppService : System.ServiceProcess.ServiceBase
    {
        ServiceHost serviceHost;
        EdwardsIndexAppDataStore dataStore;
        static IDataStore dataStoreSqLite;
        LoggingServiceTracer dbTracer;

        public EdwardsIndexAppService()
        {
            InitializeComponent();
            dbTracer = new LoggingServiceTracer();
        }

        protected override void OnStart(string[] args)
        {
            Trace.WriteLine("Service is starting ...");
            if (DataStoreFactory.CreateDataStore() == null)
            {
                Trace.WriteLine("Service could not get started.");
                throw new InvalidOperationException("Failed to open datastore");
            }

            try
            {
                string IncludeExceptionDetailInFaults = ConfigurationManager.AppSettings.Get("IncludeExceptionDetailInFaults");
                string security_mode = ConfigurationManager.AppSettings.Get("DataStoreSecurityMode");
				
				if (security_mode == "Brokered_Auth") 
				{
					serviceHost = new ServiceHost(typeof(EdwardsIndexAppDataStore));
					
					if (IncludeExceptionDetailInFaults != null && IncludeExceptionDetailInFaults.ToUpper() == "TRUE")
					{
						ServiceBehaviorAttribute sba = serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
						sba.IncludeExceptionDetailInFaults = true;
					}
					
                    FederatedServiceCredentials.ConfigureServiceHost(serviceHost, "EdwardsIndexAppDataStoreService.EdwardsIndexAppDataStore");

					serviceHost.Open();
					
					return;
				}

				string host = ConfigurationManager.AppSettings.Get("DataStoreHost");
                string port = ConfigurationManager.AppSettings.Get("DataStorePort");

                string CertLoc = ConfigurationManager.AppSettings.Get("CertLoc");
                string CertGroup = ConfigurationManager.AppSettings.Get("CertGroup");
                string Fingerprint = ConfigurationManager.AppSettings.Get("Fingerprint").Replace(" ", "");


                string auxPath = "net.tcp://" + host + ":" + port + "/EdwardsIndexAppService";

                dataStore = new EdwardsIndexAppDataStore(dataStoreSqLite);
				
				if (DataStoreFactory.ForceCreateSchema) {

					XPDictionary dict = new ReflectionDictionary();
					DBTable[] tables = dict.GetDataStoreSchema(typeof(EdwardsIndexAppCommon.XPO.IndexMarker).Assembly); //get all persistent classes from the assembly
					dataStore.UpdateSchema(false, tables);

				}

				
                serviceHost = new ServiceHost(dataStore);

                if (IncludeExceptionDetailInFaults != null && IncludeExceptionDetailInFaults.ToUpper() == "TRUE")
                {
                    ServiceBehaviorAttribute sba = serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                    sba.IncludeExceptionDetailInFaults = true;
                }

                NetTcpBinding binding;

                switch (security_mode)
                {
                    case "Transport":
                        binding = new NetTcpBinding(SecurityMode.Transport);
                        binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                        binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

                        binding.MaxReceivedMessageSize = Int32.MaxValue;
                        binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                        binding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                        binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
                        binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;

                        StoreLocation sloc = StoreLocation.LocalMachine;
                        StoreName sname = StoreName.My;

                        switch (CertLoc)
                        {
                            case "LocalComputer":
                                sloc = StoreLocation.LocalMachine;
                                break;
                        }
                        switch (CertGroup)
                        {
                            case "My":
                                sname = StoreName.My;
                                break;
                            case "Root":
                                sname = StoreName.Root;
                                break;
                        }

                        serviceHost.Credentials.ServiceCertificate.SetCertificate(sloc, sname, X509FindType.FindByThumbprint, Fingerprint);
                        serviceHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.PeerTrust;



                        break;
                    case "None":
                        binding = new NetTcpBinding(SecurityMode.None);
                        break;
                    case "Message":
                        binding = new NetTcpBinding(SecurityMode.Message);
                        binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                        binding.Security.Message.ClientCredentialType = MessageCredentialType.None;
                        break;
                    default:
                        binding = new NetTcpBinding();
                        break;
                }

                binding.MaxReceivedMessageSize = Int32.MaxValue;
                serviceHost.AddServiceEndpoint(typeof(IDataStoreService), binding, auxPath);
                serviceHost.Open();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Staring service failed: " + e.Message);
            }
        }

        protected override void OnStop()
        {
            if (serviceHost == null)
                return;
            if (serviceHost.State != CommunicationState.Opened)
                return;
            serviceHost.Close();
            serviceHost = null;
        }
    }
}
