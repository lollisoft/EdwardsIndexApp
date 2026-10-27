using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace EdwardsIndexAppSecurity
{
    public class SecureCredentials : ISecureCredentials
    {
        protected SecureString _Password;
        protected String _Username;

        public void SetCredentials(String user, String pass)
        {
            _Username = user;
            _Password = new SecureString();

            Encrypt(_Password, pass);
        }

        private void Encrypt(SecureString to, String value)
        {
            char[] chars = value.ToCharArray();
            foreach (char c in chars)
            {
                to.AppendChar(c);
            }
        }

        public String UserName
        {
            get
            {
                return _Username;
            }
        }

        public SecureString Password
        {
            get
            {
                return _Password;
            }
        }
    }
}
