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
using System.Diagnostics;

namespace StsService
{
    #region STSTokenIssuerConfiguration class
    public class STSTokenIssuerConfiguration : ConfigurationElement
    {
        private ConfigurationProperty _serviceTokens = new ConfigurationProperty("serviceTokens", typeof(ServiceTokenCollection), null, ConfigurationPropertyOptions.None);
        private ConfigurationProperty _ttlInSeconds = new ConfigurationProperty("ttlInSeconds", typeof(int), 300, ConfigurationPropertyOptions.None);
        private ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        TraceSource trace;

        private static object _lock = new object();

        public STSTokenIssuerConfiguration()
            : base()
        {
            trace = new TraceSource("StsService");

            _properties.Add(_serviceTokens);
            _properties.Add(_ttlInSeconds);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }

        public SecurityToken GetAuthorityToken()
        {
            EndpointAddress endpoint = new EndpointAddress(Constants.Trust.Actions.Issue);
            trace.TraceInformation(string.Format("SecurityTokenService.GetAuthorityToken() Endpoint {0}", endpoint.Uri));
            return GetServiceToken(endpoint);
        }

        public List<SecurityToken> GetAllServiceTokens()
        {
            ServiceTokenCollection serviceTokens = (ServiceTokenCollection)base[_serviceTokens];
            List<SecurityToken> securityTokens = new List<SecurityToken>();

            foreach (ServiceTokenConfiguration serviceToken in serviceTokens)
            {
                X509SecurityToken securityToken = (X509SecurityToken)GetSecurityTokenFromDefinition(serviceToken);
                securityTokens.Add(securityToken);
            }

            return securityTokens;
        }

        private X509SecurityToken GetXPOSecurityTokenFromDefinition(ApprovedAudiences serviceToken)
        {
            StoreName sName;
            StoreLocation sLoc;
            X509FindType fType;

            if (serviceToken == null)
                return null;

            if (!Enum.TryParse<StoreName>(serviceToken._certificate_store_name, out sName))
                return null;
            if (!Enum.TryParse<StoreLocation>(serviceToken._certificate_store_location, out sLoc))
                return null;
            if (!Enum.TryParse<X509FindType>(serviceToken._certificate_find_type, out fType))
                return null;

            X509Store store = new X509Store(sName, sLoc);

            X509SecurityToken token = null;
            try
            {
                trace.TraceInformation(string.Format("STSTokenIssuerConfiguration.GetXPOSecurityTokenFromDefinition() Token search value for {0} from database is {1}", serviceToken._uri, serviceToken._certificate_find_value));


                store.Open(OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = store.Certificates.Find(fType, serviceToken._certificate_find_value, true);

                X509Certificate2 certificate = null;
                if (collection.Count > 0)
                    certificate = collection[0];
                else
                {
                    throw new Exception(String.Format(Resources.CertificateNotFound, serviceToken._certificate_find_value));
                }

                token = new X509SecurityToken(certificate);

            }
            catch (Exception e)
            {
                trace.TraceEvent(TraceEventType.Error, 1, string.Format("STSTokenIssuerConfiguration.GetXPOSecurityTokenFromDefinition() Token for {0} from database not found ({1}).", serviceToken._uri, serviceToken._certificate_find_value));
            }
            finally
            {
                store.Close();
            }

            return token;
        }
		
        protected SecurityToken GetXPOServiceToken(EndpointAddress appliesTo)
        {
            if (appliesTo == null)
                throw new ArgumentNullException("appliesTo");

            //TODO: Add all roles for the current ligged in user
            IIdentity p1 = ServiceSecurityContext.Current.PrimaryIdentity;

            trace.TraceInformation(string.Format("STSTokenIssuerConfiguration.GetXPOServiceToken() Get mapped token to user {0} for {1}", p1.Name, appliesTo.Uri.AbsoluteUri));

            usersService usersService = new usersService();

            var users = usersService.Load();

            var user = users.FirstOrDefault(u => u._userid == p1.Name);

            var token_config = user.SecurityUsers_ApprovedAudiences_SecurityUsers.FirstOrDefault(t => t.ApprovedAudiences._uri == appliesTo.Uri.AbsoluteUri);


            if (token_config == null)
            {
				trace.TraceInformation(string.Format("STSTokenIssuerConfiguration.GetXPOServiceToken() No mapped token to user found."));
				return null;
			}

            //ServiceTokenConfiguration serviceToken = ((ServiceTokenCollection)base[_serviceTokens]).Item(appliesTo);

            X509SecurityToken token = GetXPOSecurityTokenFromDefinition(token_config.ApprovedAudiences);

            return token;
        }

        public SecurityToken GetServiceToken(EndpointAddress appliesTo)
        {
            if (appliesTo == null)
                throw new ArgumentNullException("appliesTo");

            if (!((ServiceTokenCollection)base[_serviceTokens]).Contains(appliesTo))
                return GetXPOServiceToken(appliesTo);

            ServiceTokenConfiguration serviceToken = ((ServiceTokenCollection)base[_serviceTokens]).Item(appliesTo);

            X509SecurityToken token = GetSecurityTokenFromDefinition(serviceToken);

            return token;
        }

        public int TtlInSeconds
        {
            get
            {
                return (int)base[_ttlInSeconds];
            }
        }

        private X509SecurityToken GetSecurityTokenFromDefinition(ServiceTokenConfiguration serviceToken)
        {
            X509Store store = new X509Store(serviceToken.StoreName, serviceToken.StoreLocation);

            X509SecurityToken token = null;
            try
            {
                store.Open(OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = store.Certificates.Find(serviceToken.FindType, serviceToken.FindValue, true);

                X509Certificate2 certificate = null;
                if (collection.Count > 0)
                    certificate = collection[0];
                else
                {
                    throw new Exception(String.Format(Resources.CertificateNotFound, serviceToken.FindValue));
                }

                token = new X509SecurityToken(certificate);

            }
            finally
            {
                store.Close();
            }

            return token;
        }

    }
    #endregion
}
