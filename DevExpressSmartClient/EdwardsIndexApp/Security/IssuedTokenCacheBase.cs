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
    public abstract class IssuedTokenCacheBase : IssuedTokenCache
    {
        Dictionary<Key, GenericXmlSecurityToken> cache;
        Object thisLock;

        protected IssuedTokenCacheBase()
        {
            cache = new Dictionary<Key, GenericXmlSecurityToken>();
            thisLock = new Object();
        }

        protected Dictionary<Key, GenericXmlSecurityToken> Cache
        {
            get
            {
                return this.cache;
            }
        }

        protected abstract void OnTokenAdded(Key key, GenericXmlSecurityToken token);

        protected abstract void OnTokenRemoved(Key key);

        public override void AddToken(GenericXmlSecurityToken token, EndpointAddress target, EndpointAddress issuer)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            lock (thisLock)
            {
                Key key = new Key(target.Uri, (issuer == null) ? null : issuer.Uri);
                this.cache.Add(key, token);
                OnTokenAdded(key, token);
            }
        }

        public override bool TryGetToken(EndpointAddress target, EndpointAddress issuer, out GenericXmlSecurityToken cachedToken)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            cachedToken = null;
            lock (thisLock)
            {
                GenericXmlSecurityToken tmp;
                Key key = new Key(target.Uri, issuer == null ? null : issuer.Uri);
                if (this.cache.TryGetValue(key, out tmp))
                {
                    // if the cached token is going to expire soon, remove it from the cache and return a cache miss
                    if (tmp.ValidTo < DateTime.UtcNow + TimeSpan.FromMinutes(1))
                    {
                        this.cache.Remove(key);
                        OnTokenRemoved(key);
                    }
                    else
                    {
                        cachedToken = tmp;
                    }
                }
            }
            return (cachedToken != null);
        }


        protected class Key
        {
            Uri target;
            Uri issuer;

            public Key(Uri target, Uri issuer)
            {
                this.target = target;
                this.issuer = issuer;
            }

            public Uri Target
            {
                get
                {
                    return this.target;
                }
            }

            public Uri Issuer
            {
                get
                {
                    return this.issuer;
                }
            }

            public override bool Equals(object obj)
            {
                Key other = obj as Key;
                if (other == null)
                {
                    return false;
                }
                bool result = (target == null && other.target == null) || target.Equals(other.target);
                if (result)
                {
                    result = (issuer == null && other.issuer == null) || issuer.Equals(other.issuer);
                }
                return result;
            }

            public override int GetHashCode()
            {
                int hc1 = (this.target == null) ? 0 : this.target.GetHashCode();
                int hc2 = (this.issuer == null) ? 0 : this.issuer.GetHashCode();
                return (hc1 ^ hc2);
            }
        }
    }
}
