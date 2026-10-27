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
    #region ServiceTokenCollection class
    public class ServiceTokenCollection : ConfigurationElementCollection
    {
        public ServiceTokenCollection()
            : base()
        {
        }

        public void Add(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceTokenConfiguration();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            ServiceTokenConfiguration serviceTokenConfiguration = (ServiceTokenConfiguration)element;
            return new EndpointAddress(serviceTokenConfiguration.Uri.ToString());
        }

        internal bool Contains(EndpointAddress endpoint)
        {
            return Item(endpoint) != null;
        }

        internal ServiceTokenConfiguration Item(EndpointAddress endpoint)
        {
            foreach (ServiceTokenConfiguration ele in this)
            {
                if (ele.Uri == endpoint.Uri)
                {
                    return ele;
                }
            }

            return null;
        }
    }
    #endregion
}
