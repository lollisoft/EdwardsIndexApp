using StsService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;
using lbDMFManagerSecurity.Authentication;
using StsServiceInterfaces;
using System.Threading;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using DevExpress.Xpo;

namespace StsService
{
    /// <summary>
    /// This service provides basic user self service functionality, such as changing the password and
    /// editing the profile data.
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserProfileService()
        {
        }

        #region IUserProfileService Members

        /// <summary>
        /// Change the password for the given user by providing the old password for authorizing the change.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangePasswordReply ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var reply = new ChangePasswordReply();

                STSUserNameValidator validator = new STSUserNameValidator();

                validator.Validate(request.UserId, request.OldPassword);

                usersService usersService = new usersService();

                var users = usersService.Load();

                var user = users.FirstOrDefault(u => u._userid == request.UserId);

                if (user != null)
                {
                    //TODO: Password rules must be applied before any change.
                    user._passwort = request.NewPassword;
                    user.Save();
                    
                    validator.Validate(request.UserId, request.NewPassword);

                    reply.PasswordChanged = true;
                }

                return reply;
            }
            catch (Exception ex)
            {
                // Do exception sanity check

                throw new FaultException("Failed to change the password.");
            }
        }

        public RequestAccountReply RequestAccount(RequestAccountRequest request)
        {
            try
            {
                var reply = new RequestAccountReply();

                usersService usersService = new usersService();

                ApplicationsService appService = new ApplicationsService();

                var apps = appService.Load();

                if (!apps.Any(a => a._name == request.ApplicationId && a._enablerequestaccount == true))
                {
                    return reply;
                }

                var users = usersService.Load();

                if (users.Any(u => u._userid == request.UserId || u._email == request.EMailAddress))
                {
                    SecurityUsers user = users.First(u => u._userid == request.UserId || u._email == request.EMailAddress);

                    CreateResetPasswordEMail(user);

                    return reply;
                }

                if (!users.Any(u => u._userid == request.UserId && u._email == request.EMailAddress))
                {
                    SecurityUsers user = new SecurityUsers
                    {
                        _email = request.EMailAddress,
                        _userid = request.UserId,
                        _passwort = Guid.NewGuid().ToString()
                    };

                    user.Save();

                    foreach (var app in apps.Where(a => a._name == request.ApplicationId && a._enablerequestaccount == true).ToList())
                    {
                        foreach (var app_audiences in app.Applications_ApprovedAudiences_Applications)
                        {
                            SecurityUsers_ApprovedAudiences sa = new SecurityUsers_ApprovedAudiences
                            {
                                _name = "Autolinked " + app_audiences.Applications._name,
                                ApprovedAudiences = app_audiences.ApprovedAudiences,
                                SecurityUsers = user
                            };

                            sa.Save();
                        }
                    }

                    CreateResetPasswordEMail(user);
                }

                return reply;
            }
            catch (Exception ex)
            {
                // Do exception sanity check

                throw new FaultException("Failed to request an account: " + ex.Message);
            }
        }

        public ResetPasswordReply ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                ResetPasswordReply reply = new ResetPasswordReply();

                if (request.IsResetRequest)
                {
                    usersService usersService = new usersService();

                    var users = usersService.Load();
                    var user = users.FirstOrDefault(u => u._email == request.EMailAddress);

                    if (user != null)
                    {
                        CreateResetPasswordEMail(user);
                    }
                }
                else if (request.IsResetComplete)
                {
                    usersService usersService = new usersService();

                    var date = DateTime.Now;

                    var users = usersService.Load();
                    var user = users.FirstOrDefault(u => u._email == request.EMailAddress && u._userid == request.UserId && u._passwordresettoken == request.ResetPasswordToken && u._passwordreset >= date);

                    if (user != null)
                    {
                        user._passwort = request.NewPassword;
                        user._passwordreset = date;
                        user._passwordresettoken = null;
                        user.Save();

                        STSUserNameValidator validator = new STSUserNameValidator();

                        validator.Validate(request.UserId, request.NewPassword);

                        ApplicationsService appService = new ApplicationsService();

                        var apps = appService.Load();

                        foreach (var app in apps.Where(a => a._enablerequestaccount == true).ToList())
                        {
                            foreach (var app_audiences in app.Applications_ApprovedAudiences_Applications)
                            {
                                XPCollection<SecurityUsers_ApprovedAudiences> collsa = new XPCollection<SecurityUsers_ApprovedAudiences>();

                                if (!collsa.Any(u => u.SecurityUsers.ID == user.ID || u.ApprovedAudiences.ID == app_audiences.ID))
                                {
                                    SecurityUsers_ApprovedAudiences sa = new SecurityUsers_ApprovedAudiences
                                    {
                                        _name = "Autolinked " + app_audiences.Applications._name,
                                        ApprovedAudiences = app_audiences.ApprovedAudiences,
                                        SecurityUsers = user
                                    };

                                    sa.Save();
                                }
                            }
                        }
                    }
                }

                return reply;
            }
            catch (Exception ex)
            {
                // Do exception sanity check

                throw new FaultException("Failed to reset the password.");
            }
        }

        private static void CreateResetPasswordEMail(SecurityUsers user)
        {
            //TODO: Get these values from app configuration
            using (MailMessage message = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["EMailAddress"]), new MailAddress(user._email)))
            {
                Guid resetToken = Guid.NewGuid();
                message.Subject = "Password reset requested";
                message.Body = string.Format(
                    "You may have requested a new password either for a new account or your lost password.\n" +
                    "If you are really the initiator, then proceed the reset process.\n\n" +
                    "Please enter the following token into the token field: {0}\n" +
                    "Also repeat your userId ({1}) and eMail ({2}) address to proceed the reset password procedure.", resetToken, user._userid, user._email);

                using (SmtpClient client = new SmtpClient())
                {
                    try
                    {
                        //TODO: Get these values from app configuration
                        client.Host = ConfigurationManager.AppSettings["EMailServer"];
                        client.Port = 25;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EMailUser"], ConfigurationManager.AppSettings["EMailPass"]);

                        client.Send(message);

                        user._passwordreset = DateTime.Now.AddDays(1);
                        user._passwordresettoken = resetToken.ToString();
                        user.Save();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        #endregion
    }

    public class StsServiceAuthorizationManager : ServiceAuthorizationManager
    {
        // Override AccessCheck to enforce access control requirements.
        public override bool CheckAccess(OperationContext operationContext)
        {
            AuthorizationContext authContext =
            operationContext.ServiceSecurityContext.AuthorizationContext;

			try {
				TraceSource trace = new TraceSource("StsService");
				OperationContext context = OperationContext.Current;
				MessageProperties properties = context.IncomingMessageProperties;
				RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
				string address = endpoint.Address;

				trace.TraceInformation(string.Format("StsServiceAuthorizationManager.CheckAccess() Called. Client IP = {0}", address));
            }
			catch (Exception e) {
			
			}
			
            if (authContext.ClaimSets == null)
                return false;
            //if (authContext.ClaimSets.Count != 1)
            //    return false;
            ClaimSet myClaimSet = authContext.ClaimSets[1];
            //if (!IssuedBySTS_A(myClaimSet))
            //    return false;
            if (myClaimSet.Count != 4)
                return false;

            bool rolesFound = false;
            bool customFound = false;

            // Check for generally required claims
            foreach (var myClaim in myClaimSet)
            {
                if (myClaim.ClaimType == "http://schemas.microsoft.com/xsi/2005/05/role_meins/role")
                {
                    rolesFound = true;
                }

                //if (myClaim.ClaimType == "MyClaimType")
                //{
                customFound = true;
                //}
            }

            if (rolesFound && customFound)
            {
                //TODO: Map actions against roles to be required from a database.
                string action = operationContext.IncomingMessageHeaders.Action;

                operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = Thread.CurrentPrincipal;

                return true;
            }

            return false;
        }
    }

}
