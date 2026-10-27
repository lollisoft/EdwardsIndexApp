using lbDMFManagerSecurity.Authentication;
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

namespace StsService
{
    public class STSUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            usersService usersService = new usersService();

            var users = usersService.Load();

            var user = users.FirstOrDefault(u => u._userid == userName);
            
            if (user != null && user._userid == userName && user._passwort == password)
            {
                user._passwort = PasswordHash.CreateHash(user._passwort);
				user.Save();
				
                return;
			}

            if (user != null && PasswordHash.ValidatePassword(password, user._passwort))
            {
				return;
			}
			
			
            throw new FaultException("Not authenticated");
        }
    }
}
