using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace EdwardsIndexAppSecurity
{
    public interface ISecureCredentials
    {
        void SetCredentials(String user, String pass);

        String UserName
        {
            get;
        }

        SecureString Password
        {
            get;
        }
    }
}
