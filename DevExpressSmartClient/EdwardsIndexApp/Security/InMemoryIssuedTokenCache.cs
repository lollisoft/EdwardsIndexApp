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
    // A simple in-memory cache for caching issued tokens at a wider scope than the channel.
    // A real cache implementation will have quotas, eviction policies etc.
    public class InMemoryIssuedTokenCache : IssuedTokenCacheBase
    {
        public InMemoryIssuedTokenCache()
            : base()
        {
        }

        protected override void OnTokenAdded(Key key, GenericXmlSecurityToken token)
        {
        }

        protected override void OnTokenRemoved(Key key)
        {
        }
    }
}
