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
    internal static class Constants
    {
        public const string AssertionIdPrefix = "SamlSecurityToken-";

        public static class TokenTypes
        {
            public const string SAML = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";
        }

        public static class Addressing
        {
            public const string NamespaceUri = "http://www.w3.org/2005/08/addressing";
            public const string NamespaceUriAugust2004 = "http://schemas.xmlsoap.org/ws/2004/08/addressing";

            public class Elements
            {
                public const string EndpointReference = "EndpointReference";
            }
        }

        public static class IdentityModel
        {
            public const string NamespaceUri = "http://schemas.microsoft.com/ws/2005/05/identity";

            public class Attributes
            {
                public const string Uri = "Uri";
                public const string Optional = "Optional";
            }

            public class ClaimTypes
            {
                public const string PPI = "http://schemas.microsoft.com/ws/2005/05/identity/claims/privatepersonalidentifier";
            }

            public class Elements
            {
                public const string Claim = "ClaimType";
            }
        }

        public static class Policy
        {
            public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/09/policy";

            public class Elements
            {
                public const string AppliesTo = "AppliesTo";
            }
        }

        public static class SecurityUtil
        {
            public const string NamespaceUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        }

        // Various constants for WS-Trust
        public static class Trust
        {
            public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2005/02/trust";

            public class Actions
            {
                public const string Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";
                public const string IssueReply = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";
                public const string Validate = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate";
                public const string ValidateReply = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate";
            }

            public class Attributes
            {
                public const string Context = "Context";
                public const string Type = "Type";
            }

            public class Elements
            {
                public const string KeySize = "KeySize";
                public const string KeyType = "KeyType";
                public const string UseKey = "UseKey";
                public const string Entropy = "Entropy";
                public const string BinarySecret = "BinarySecret";
                public const string RequestSecurityToken = "RequestSecurityToken";
                public const string RequestSecurityTokenResponse = "RequestSecurityTokenResponse";
                public const string RequestType = "RequestType";
                public const string TokenType = "TokenType";
                public const string RequestedSecurityToken = "RequestedSecurityToken";
                public const string RequestedAttachedReference = "RequestedAttachedReference";
                public const string RequestedUnattachedReference = "RequestedUnattachedReference";
                public const string RequestedProofToken = "RequestedProofToken";
                public const string ComputedKey = "ComputedKey";
                public const string Claims = "Claims";
                public const string LifeTime = "Lifetime";
                public const string Created = "Created";
                public const string Expires = "Expires";
            }

            public class RequestTypes
            {
                public const string Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";
                public const string Validate = "http://schemas.xmlsoap.org/ws/2005/02/trust/Validate";
            }

            public class KeyTypes
            {
                public const string Public = "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";
                public const string Symmetric = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";
            }

            public class BinarySecretTypes
            {
                public const string AsymmetricKey = "http://schemas.xmlsoap.org/ws/2005/02/trust/AsymmetricKey";
                public const string SymmetricKey = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";
                public const string Nonce = "http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce";
            }

            public class ComputedKeyAlgorithms
            {
                public const string PSHA1 = "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";
            }
        }

        // Various constants for XML-DigitalSignature
        public static class XmlDSig
        {
            public const string NamespaceUri = "http://www.w3.org/2000/09/xmldsig#";

            public static class Elements
            {
                public const string KeyInfo = "KeyInfo";
                public const string KeyValue = "KeyValue";
                public const string RsaKeyValue = "RSAKeyValue";
                public const string Modulus = "Modulus";
                public const string Exponent = "Exponent";
            }
        }

    }
}
