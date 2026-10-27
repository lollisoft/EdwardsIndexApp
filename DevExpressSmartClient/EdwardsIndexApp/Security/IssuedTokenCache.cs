using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;
using System.Xml;


namespace EdwardsIndexAppSecurity
{
    public abstract class IssuedTokenCache
    {
        public abstract void AddToken(GenericXmlSecurityToken token, EndpointAddress target, EndpointAddress issuer);
        public abstract bool TryGetToken(EndpointAddress target, EndpointAddress issuer, out GenericXmlSecurityToken cachedToken);
    }

}
