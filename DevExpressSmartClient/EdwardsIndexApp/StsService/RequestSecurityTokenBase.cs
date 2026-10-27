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
    internal abstract class RequestSecurityTokenBase : BodyWriter
    {
        // private members
        protected string context;
        protected string tokenType;
        protected int keySize;
        protected EndpointAddress appliesTo;

        // Constructors
        protected RequestSecurityTokenBase()
            : this(String.Empty, String.Empty, 0, null)
        {
        }

        protected RequestSecurityTokenBase(string context, string tokenType, int keySize, EndpointAddress appliesTo)
            : base(true)
        {
            this.context = context;
            this.tokenType = tokenType;
            this.keySize = keySize;
            this.appliesTo = appliesTo;
        }

        // public properties
        public string Context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
            }
        }

        public string TokenType
        {
            get
            {
                return tokenType;
            }
            set
            {
                tokenType = value;
            }
        }

        public int KeySize
        {
            get
            {
                return keySize;
            }
            set
            {
                keySize = value;
            }
        }

        public EndpointAddress AppliesTo
        {
            get
            {
                return appliesTo;
            }
            set
            {
                appliesTo = value;
            }
        }
    }
}
