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

    #region ServiceTokenConfiguration class,
    public class ServiceTokenConfiguration : ConfigurationElement
    {
        private ConfigurationProperty _uri = new ConfigurationProperty("uri", typeof(Uri), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
        private ConfigurationProperty _storeLocation = new ConfigurationProperty("storeLocation", typeof(StoreLocation), StoreLocation.LocalMachine, ConfigurationPropertyOptions.None);
        private ConfigurationProperty _storeName = new ConfigurationProperty("storeName", typeof(StoreName), StoreName.My, ConfigurationPropertyOptions.None);
        private ConfigurationProperty _findValue = new ConfigurationProperty("findValue", typeof(string), null, ConfigurationPropertyOptions.None);
        private ConfigurationProperty _findType = new ConfigurationProperty("findType", typeof(X509FindType), X509FindType.FindBySubjectName, ConfigurationPropertyOptions.None);

        private ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        public ServiceTokenConfiguration()
            : base()
        {
            _properties.Add(_uri);
            _properties.Add(_storeLocation);
            _properties.Add(_storeName);
            _properties.Add(_findValue);
            _properties.Add(_findType);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }

        internal StoreLocation StoreLocation
        {
            get
            {
                return (StoreLocation)base[_storeLocation];
            }
        }

        internal StoreName StoreName
        {
            get
            {
                return (StoreName)base[_storeName];
            }
        }

        internal string FindValue
        {
            get
            {
                return (string)base[_findValue];
            }
        }

        internal X509FindType FindType
        {
            get
            {
                return (X509FindType)base[_findType];
            }
        }

        internal Uri Uri
        {
            get
            {
                return base[_uri] as Uri;
            }
        }
    }
    #endregion
}
