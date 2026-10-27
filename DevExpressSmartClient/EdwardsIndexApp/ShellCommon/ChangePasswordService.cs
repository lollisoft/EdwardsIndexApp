using DevExpress.XtraEditors;
using EdwardsIndexAppSecurity;
using StsServiceInterfaces;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
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
using CABDevExpress;

namespace ShellCommon
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly WorkItem workItem;

        [InjectionConstructor]
        public ChangePasswordService([ServiceDependency] WorkItem workItem)
		{
			this.workItem = workItem;
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

        private void SetupTokenAuthorization(ChannelFactory<IUserProfileService> serviceFactory)
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

        public bool ChangePassword()
        {
            ChangePasswordForm chPw = new ChangePasswordForm();

            if (chPw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var serviceFactory = new ChannelFactory<IUserProfileService>("UserProfileEndpoint"))
                {
                    try
                    {
                        SetupTokenAuthorization(serviceFactory);

                        IUserProfileService profileService = serviceFactory.CreateChannel();

                        var cred = workItem.Services.Get<ISecureCredentials>(true);

                        var reply = profileService.ChangePassword(new ChangePasswordRequest
                        {
                            UserId = cred.UserName,
                            NewPassword = chPw.NewPassword,
                            OldPassword = chPw.OldPassword
                        });

                        serviceFactory.Close();

                        if (reply.PasswordChanged)
                        {
                            cred.SetCredentials(cred.UserName, chPw.NewPassword);
                        }

                        return reply.PasswordChanged;
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

            return false;
        }
    }
}
