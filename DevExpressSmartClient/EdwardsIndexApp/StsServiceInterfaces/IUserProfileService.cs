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

namespace StsServiceInterfaces
{
    /// <summary>
    /// This service provides basic user self service functionality, such as changing the password and
	/// editing the profile data.
    /// </summary>
    [ServiceContract(Namespace="http://cloud.lollisoft.de")]
    public interface IUserProfileService
    {
        [OperationContract(Action = "RequestAccountRequest", ReplyAction = "RequestAccountReply")]
        RequestAccountReply RequestAccount(RequestAccountRequest request);
        
        [OperationContract(Action = "ResetPasswordRequest", ReplyAction = "ResetPasswordReply")]
        ResetPasswordReply ResetPassword(ResetPasswordRequest request);
        
        [OperationContract(Action = "ChangePasswordRequest", ReplyAction = "ChangePasswordReply")]
        ChangePasswordReply ChangePassword(ChangePasswordRequest request);
    }
}
