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
    /// <summary>
    /// STS Configuration section
    /// </summary>
    public sealed class STSConfiguration : ConfigurationSection
    {
        const string SectionName = "StsService";

        private ConfigurationProperty _samlTokenIssuerConfiguration = new ConfigurationProperty("samlTokenIssuer", typeof(STSTokenIssuerConfiguration), null, ConfigurationPropertyOptions.None);
        private ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        static STSConfiguration _configuration = null;
        static Exception _configurationException = null;
        static object _configurationLock = new object();

        static STSConfiguration Current
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_configurationLock)
                    {
                        if (_configuration == null)
                        {
                            STSConfiguration configuration = null;

                            if (_configurationException == null)
                            {
                                try
                                {
                                    if (configuration == null)
                                    {
                                        configuration = (STSConfiguration)ConfigurationManager.GetSection(STSConfiguration.SectionName);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //
                                    // Cache the exception for perf reason
                                    //
                                    _configurationException = ex;

                                    //
                                    // rethrow
                                    //
                                    throw new ConfigurationErrorsException(Resources.ConfigurationUnexpectedError, ex);
                                }
                            }
                            else
                            {
                                //
                                // rethrow
                                //
                                throw new ConfigurationErrorsException(Resources.ConfigurationUnexpectedError, _configurationException);
                            }

                            if (configuration == null)
                            {
                                configuration = new STSConfiguration();
                            }

                            _configuration = configuration;
                        }
                    }
                }

                return _configuration;
            }
        }

        public STSConfiguration()
            : base()
        {
            _properties.Add(_samlTokenIssuerConfiguration);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }

        private static void Initialize()
        {
            // Just load up the config, if not already loaded.
            STSConfiguration config = STSConfiguration.Current;
        }
        public static STSTokenIssuerConfiguration SamlTokenIssuerConfiguration
        {
            get
            {
                Initialize();
                return (STSTokenIssuerConfiguration)_configuration[_configuration._samlTokenIssuerConfiguration];
            }
        }

    }
}
