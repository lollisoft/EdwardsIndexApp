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
    /// <summary>
    /// Secure Token Service Implementation
    /// </summary>
    public class SecurityTokenService : ISecurityTokenService
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SecurityTokenService()
        {
        }

        #region ISecurityTokenService Members

		// http://weblogs.asp.net/cibrax/actas-in-ws-trust-1-4
		// Additionally look for certificate auth instead password auth for identity delegation
		
        /// <summary>
        /// Issues the SAML token according to the received RST message.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Message IssueToken(Message request)
        {
            try
            {
                // if request is null, we're toast
                if (request == null)
                    throw new ArgumentNullException("request");

				try
                {
                    TraceSource trace = new TraceSource("StsService");

                    OperationContext context = OperationContext.Current;
                    MessageProperties properties = context.IncomingMessageProperties;
                    RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    string address = endpoint.Address;

                    trace.TraceInformation(string.Format("SecurityTokenService.IssueToken() Called. Client IP = {0}", address));
                }
                catch (Exception e)
                {

                }

                // Create an RST object from the request message
                RequestSecurityToken rst = RequestSecurityToken.CreateFrom(request.GetReaderAtBodyContents());

                // Check that is really is an Issue request
                if (rst.RequestType == null || rst.RequestType != Constants.Trust.RequestTypes.Issue)
                    throw new InvalidOperationException(rst.RequestType);

                // http://stackoverflow.com/questions/5310700/problem-using-custom-principal-and-identity-with-wcf-services
                //string username = 

                // Create an RSTR object
                RequestSecurityTokenResponse rstr = Issue(rst);

                // Create response message
                Message response = Message.CreateMessage(request.Version, Constants.Trust.Actions.IssueReply, rstr);

                // Set RelatesTo of response to message id of request
                response.Headers.RelatesTo = request.Headers.MessageId;

                // Address response to ReplyTo of request
                request.Headers.ReplyTo.ApplyTo(response);
                return response;
            }
            catch (Exception ex)
            {
                // Do exception sanity check

                throw;
            }
        }

        #endregion

        // private methods
        private RequestSecurityTokenResponse Issue(RequestSecurityToken rst)
        {
            // If rst is null, we're toast
            if (rst == null)
                throw new ArgumentNullException("rst");

            // Create an RSTR object
            RequestSecurityTokenResponse rstr = new RequestSecurityTokenResponse();

            // Figure out the token type being requested
            string tokenType = GetTokenType(rst);

            // Figure out which key we're going to sign the token with...
            SecurityKey signingKey = GetSigningKey();
            SecurityKeyIdentifier signingKeyIdentifier = GetSigningKeyIdentifier();
            SecurityKeyIdentifier proofKeyIdentifier = null;

            if (rst.IsProofKeyAsymmetric())
            {
                // Asymmetric proof key
                proofKeyIdentifier = GetAsymmetricProofKey(rst);
            }
            else
            {
                // Symmetric proof key
                // Construct session key. This is the symmetric key that the client and the service will share. 
                // It actually appears twice in the response message; once for the service and 
                // once for the client. In the former case, it is typically embedded in the issued token, 
                // in the latter case, it is returned in a wst:RequestedProofToken element.
                byte[] sessionKey = GetSessionKey(rst, rstr);

                // Get token to use when encrypting key material for the service
                SecurityToken encryptingToken = GetEncryptingToken(rst);

                // Encrypt the session key for the service
                byte[] encryptedKey = GetEncryptedKey(encryptingToken, sessionKey, out proofKeyIdentifier);
            }

            DateTime effectiveTime = DateTime.Now;
            DateTime expirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(STSConfiguration.SamlTokenIssuerConfiguration.TtlInSeconds));
            SecurityToken securityToken = null;

            switch (tokenType)
            {
                case Constants.TokenTypes.SAML:
                    securityToken = CreateSAMLToken(effectiveTime, expirationTime, signingKey, signingKeyIdentifier, proofKeyIdentifier, rst.ClaimRequirements);
                    break;
                default:
                    throw new NotSupportedException(Resources.UnsupportedTokenType);
            }

            rstr.RequestedSecurityToken = securityToken;
            rstr.Context = rst.Context;
            rstr.TokenType = tokenType;
            rstr.RequestedAttachedReference = GetInternalSecurityKeyIdentifier(securityToken)[0];
            rstr.RequestedUnattachedReference = GetExternalSecurityKeyIdentifier(securityToken)[0];
            return rstr;
        }

        /// <summary>
        /// This method determines the token type for the service the issued token is intended for.
        /// </summary>
        /// <param name="rst"></param>
        /// <returns></returns>
        private string GetTokenType(RequestSecurityToken rst)
        {
            // If rst is null, we're toast
            if (rst == null)
                throw new ArgumentNullException("rst");

            // Set tokenType to null
            string tokenType = null;

            // If there is no service URI or we don't have a tokenType registered for that service URI...
            tokenType = rst.TokenType;

            // return the tokenType
            return tokenType;
        }

        private byte[] GetSenderEntropy(RequestSecurityToken rst)
        {
            // If rst is null, we're toast
            if (rst == null)
                throw new ArgumentNullException("rst");

            SecurityToken senderEntropyToken = rst.RequestorEntropy;
            byte[] senderEntropy = null;

            if (senderEntropyToken != null)
            {
                BinarySecretSecurityToken bsst = senderEntropyToken as BinarySecretSecurityToken;

                if (bsst != null)
                    senderEntropy = bsst.GetKeyBytes();
            }

            return senderEntropy;
        }

        private byte[] GetIssuerEntropy(int keySize)
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] issuerEntropy = new byte[keySize / 8];
            random.GetNonZeroBytes(issuerEntropy);
            return issuerEntropy;
        }

        private byte[] GetSessionKey(RequestSecurityToken rst, RequestSecurityTokenResponse rstr)
        {
            // If rst is null, we're toast
            if (rst == null)
                throw new ArgumentNullException("rst");

            // If rstr is null, we're toast
            if (rstr == null)
                throw new ArgumentNullException("rstr");

            // Figure out the keySize
            int keySize = 256;

            if (rst.KeySize != 0)
                keySize = rst.KeySize;

            // Figure out whether we're using Combined or Issuer entropy.
            byte[] sessionKey = null;
            byte[] senderEntropy = GetSenderEntropy(rst);
            byte[] issuerEntropy = GetIssuerEntropy(keySize);

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            if (senderEntropy != null)
            {
                // Combined entropy
                sessionKey = rstr.ComputeCombinedKey(senderEntropy, issuerEntropy, keySize);
                rstr.IssuerEntropy = new BinarySecretSecurityToken(issuerEntropy);
                rstr.ComputeKey = true;
            }
            else
            {
                // Issuer-only entropy
                sessionKey = issuerEntropy;
                rstr.RequestedProofToken = new BinarySecretSecurityToken(sessionKey);
            }

            rstr.KeySize = keySize;
            return sessionKey;
        }

        /// <summary>
        /// This method retrieves the key that the STS should sign the issued token with in order 
        /// for the service the issued token is intended for to trust that token 
        /// </summary>
        /// <returns></returns>
        private SecurityKey GetSigningKey()
        {
            // Find the signing cert
            SecurityToken token = GetSigningToken();

            if (token != null)
                // return the zeroth key
                return token.SecurityKeys[0];
            else
                return null;
        }

        // This method determines the security token that contains the key material that 
        // the STS should sign the issued token with in order 
        // for the service the issued token is intended for to trust that token        
        private SecurityToken GetSigningToken()
        {
            X509SecurityToken signingToken = (X509SecurityToken)STSConfiguration.SamlTokenIssuerConfiguration.GetAuthorityToken();

            // return the token
            return signingToken;
        }

        // This method determines the security token that contains the key material that 
        // the STS should encrypt a session key with in order 
        // for the service the issued token is intended for to be able to extract that session key
        private SecurityToken GetEncryptingToken(RequestSecurityToken rst)
        {
            // If rst is null, we're toast
            if (rst == null)
                throw new ArgumentNullException("rst");

            X509SecurityToken encryptingToken = (X509SecurityToken)STSConfiguration.SamlTokenIssuerConfiguration.GetServiceToken(rst.AppliesTo);

            // return the token
            return encryptingToken;
        }



        /// <summary>
        ///	This method returns the security token that contains the key material that should be used to 
        /// encrypt the session that will be used by the client when talking to the service endpoint
        /// This method is called when creating the RequestedProofToken 
        /// </summary>
        /// <returns></returns>
        private SecurityToken GetClientKeyWrapToken()
        {
            // Get the current OperationContext
            OperationContext oc = OperationContext.Current;

            // set token to null
            SecurityToken token = null;

            // If there is an initiator token...
            if (oc.IncomingMessageProperties.Security.InitiatorToken != null)
            {
                token = oc.IncomingMessageProperties.Security.InitiatorToken.SecurityToken;
            }
            // ... otherwise, if these is a protection token...
            else if (oc.IncomingMessageProperties.Security.ProtectionToken != null)
            {
                token = oc.IncomingMessageProperties.Security.ProtectionToken.SecurityToken;
            }

            // return the token
            return token;
        }


        /// <summary>
        /// This method returns a security key identifier that can be used to refer to the provided security token when 
        /// the provided token appears in the message
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        private SecurityKeyIdentifier GetInternalSecurityKeyIdentifier(SecurityToken securityToken)
        {
            if (securityToken == null)
                throw new ArgumentNullException("securityToken");

            // Set skiClause to null
            SecurityKeyIdentifierClause skiClause = null;

            // Try for a local id reference first...
            if (securityToken.CanCreateKeyIdentifierClause<LocalIdKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<LocalIdKeyIdentifierClause>();

                // return a SecurityKeyIdentifier 
                return new SecurityKeyIdentifier(skiClause);
            }
            else
                return GetExternalSecurityKeyIdentifier(securityToken);


        }

        /// <summary>
        /// This method returns a security key identifier that can be used to refer to the provided security token when 
        /// the provided token does not appear in the message
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        private SecurityKeyIdentifier GetExternalSecurityKeyIdentifier(SecurityToken securityToken)
        {
            if (securityToken == null)
                throw new ArgumentNullException("securityToken");

            // Set skiClause to null
            SecurityKeyIdentifierClause skiClause = null;

            // Try for an encrypted key reference first...
            if (securityToken.CanCreateKeyIdentifierClause<EncryptedKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<EncryptedKeyIdentifierClause>();
            }
            // ... kerb token reference next...
            else if (securityToken.CanCreateKeyIdentifierClause<KerberosTicketHashKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<KerberosTicketHashKeyIdentifierClause>();
            }
            // ... X509 thumbprint next...
            else if (securityToken.CanCreateKeyIdentifierClause<X509ThumbprintKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<X509ThumbprintKeyIdentifierClause>();
            }
            // ... X509 raw reference next...
            else if (securityToken.CanCreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>();
            }
            // ... X509 SKI next...
            else if (securityToken.CanCreateKeyIdentifierClause<X509SubjectKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<X509SubjectKeyIdentifierClause>();
            }
            // ... try for a binary secret...
            else if (securityToken.CanCreateKeyIdentifierClause<BinarySecretKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<BinarySecretKeyIdentifierClause>();
            }
            // ... then a X509IssuerSerial reference ...
            else if (securityToken.CanCreateKeyIdentifierClause<X509IssuerSerialKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<X509IssuerSerialKeyIdentifierClause>();
            }
            // ... then a SAML assertion reference...
            else if (securityToken.CanCreateKeyIdentifierClause<SamlAssertionKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<SamlAssertionKeyIdentifierClause>();
            }

            // ... then an RSA key reference...
            else if (securityToken.CanCreateKeyIdentifierClause<RsaKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<RsaKeyIdentifierClause>();
            }
            // ... then a key name reference...
            else if (securityToken.CanCreateKeyIdentifierClause<KeyNameIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<KeyNameIdentifierClause>();
            }
            // ... and finally an SCT reference...
            else if (securityToken.CanCreateKeyIdentifierClause<SecurityContextKeyIdentifierClause>())
            {
                skiClause = securityToken.CreateKeyIdentifierClause<SecurityContextKeyIdentifierClause>();
            }

            // return a SecurityKeyIdentifier
            return new SecurityKeyIdentifier(skiClause);
        }


        /// <summary>
        /// This method returns a security token to be used as the requested proof token portion of the RSTR
        /// The key parameter is the proof key that will be shared by the client and the actual service
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private SecurityToken GetRequestedProofToken(byte[] key)
        {
            // If key is null, we're toast
            if (key == null)
                throw new ArgumentNullException("key");

            // Get the security token that will be used to encrypt the provided key
            SecurityToken wrappingToken = GetClientKeyWrapToken();

            // Get the encryptionAlgorithm to use
            string keywrapAlgorithm = GetKeyWrapAlgorithm(wrappingToken.SecurityKeys[0]);
            ;

            // Get the SecurityKeyIdentifier for the wrapping token
            SecurityKeyIdentifier wrappingTokenReference = GetInternalSecurityKeyIdentifier(wrappingToken);

            // Create a new Wrapped token and return it
            return new WrappedKeySecurityToken("_" + Guid.NewGuid().ToString(), key, keywrapAlgorithm, wrappingToken, wrappingTokenReference);
        }

        private string GetKeyWrapAlgorithm(SecurityKey key)
        {
            // If key is null, we're toast
            if (key == null)
                throw new ArgumentNullException("key");

            // Set keywrapAlgorithm to null
            string keywrapAlgorithm = null;

            // If the security key supports RsaOaep then use that ...
            if (key.IsSupportedAlgorithm(SecurityAlgorithms.RsaOaepKeyWrap))
                keywrapAlgorithm = SecurityAlgorithms.RsaOaepKeyWrap;
            // ... otherwise if it supports RSA15 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.RsaV15KeyWrap))
                keywrapAlgorithm = SecurityAlgorithms.RsaV15KeyWrap;
            // ... otherwise if it supports AES256 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.Aes256KeyWrap))
                keywrapAlgorithm = SecurityAlgorithms.Aes256KeyWrap;
            // ... otherwise if it supports AES192 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.Aes192KeyWrap))
                keywrapAlgorithm = SecurityAlgorithms.Aes192KeyWrap;
            // ... otherwise if it supports AES128 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.Aes128KeyWrap))
                keywrapAlgorithm = SecurityAlgorithms.Aes128KeyWrap;

            return keywrapAlgorithm;
        }

        private string GetSignatureAlgorithm(SecurityKey key)
        {
            // If key is null, we're toast
            if (key == null)
                throw new ArgumentNullException("key");

            // Set signatureAlgorithm to null
            string signatureAlgorithm = null;

            // If the security key supports RsaSha1 then use that ...
            if (key.IsSupportedAlgorithm(SecurityAlgorithms.RsaSha1Signature))
                signatureAlgorithm = SecurityAlgorithms.RsaSha1Signature;
            // ... otherwise if it supports HMACSha1 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.HmacSha1Signature))
                signatureAlgorithm = SecurityAlgorithms.HmacSha1Signature;

            return signatureAlgorithm;
        }

        private string GetEncryptionAlgorithm(SecurityKey key)
        {
            // If key is null, we're toast
            if (key == null)
                throw new ArgumentNullException("key");

            // Set encryptionAlgorithm to null
            string encryptionAlgorithm = null;

            // If the security key supports AES256 use that ...
            if (key.IsSupportedAlgorithm(SecurityAlgorithms.Aes256Encryption))
                encryptionAlgorithm = SecurityAlgorithms.Aes256Encryption;
            // ... otherwise if it supports AES192 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.Aes192Encryption))
                encryptionAlgorithm = SecurityAlgorithms.Aes192Encryption;
            // ... otherwise if it supports AES128 use that ...
            else if (key.IsSupportedAlgorithm(SecurityAlgorithms.Aes128Encryption))
                encryptionAlgorithm = SecurityAlgorithms.Aes128Encryption;

            return encryptionAlgorithm;
        }

        /// <summary>
        /// This method returns a SecurityKeyIdentifier for the key the STS will use to sign the issued token 
        /// </summary>
        /// <returns></returns>
        private SecurityKeyIdentifier GetSigningKeyIdentifier()
        {
            // Get the token that the STS will sign the token with
            SecurityToken signingToken = GetSigningToken();

            // return a SecurityKeyIdentifier for the token
            return GetExternalSecurityKeyIdentifier(signingToken);
        }

        /// <summary>
        /// This method encrypts the provided key using the key material associated with the cert
        /// returned by DetermineEncryptingCert
        /// </summary>
        /// <param name="encryptingToken"></param>
        /// <param name="key"></param>
        /// <param name="ski"></param>
        /// <returns></returns>
        private byte[] GetEncryptedKey(SecurityToken encryptingToken, byte[] key, out SecurityKeyIdentifier ski)
        {
            // If encryptingToken is null, we're toast
            if (encryptingToken == null)
                throw new ArgumentNullException("encryptingToken");

            // If key is null, we're toast
            if (key == null)
                throw new ArgumentNullException("key");

            // Get the zeroth security key
            SecurityKey encryptingKey = encryptingToken.SecurityKeys[0];

            // Get the encryption algorithm to use
            string keywrapAlgorithm = GetKeyWrapAlgorithm(encryptingKey);

            // encrypt the passed in key 
            byte[] encryptedKey = encryptingKey.EncryptKey(keywrapAlgorithm, key);

            // get a key identifier for the encrypting key
            SecurityKeyIdentifier eki = GetExternalSecurityKeyIdentifier(encryptingToken);

            // return the proof key identifier
            ski = GetProofKeyIdentifier(encryptedKey, keywrapAlgorithm, eki);

            // return the encrypted key
            return encryptedKey;
        }

        private SecurityKeyIdentifier GetProofKeyIdentifier(byte[] key, string algorithm, SecurityKeyIdentifier eki)
        {
            // If key is null, we're toast
            if (key == null)
                throw new ArgumentNullException("key");

            // return a SecurityKeyIdentifier
            return new SecurityKeyIdentifier(new EncryptedKeyIdentifierClause(key, algorithm, eki));
        }

        private SecurityKeyIdentifier GetAsymmetricProofKey(RequestSecurityToken rst)
        {
            return GetInternalSecurityKeyIdentifier(rst.ProofKey);
        }

        private SecurityToken CreateSAMLToken(DateTime validFrom, DateTime validTo, SecurityKey signingKey, SecurityKeyIdentifier signingKeyIdentifier, SecurityKeyIdentifier proofKeyIdentifier, IList<ClaimTypeRequirement> claimReqs)
        {
            // Create list of confirmation strings
            List<string> confirmations = new List<string>();

            // Add holder-of-key string to list of confirmation strings
            confirmations.Add(SamlConstants.HolderOfKey);

            // Create SAML subject statement based on issuer member variable, confirmation string collection 
            // local variable and proof key identifier parameter
            SamlSubject subject = new SamlSubject(null, null, "StsService", confirmations, null, proofKeyIdentifier);

            // Create a list of SAML attributes
            List<SamlAttribute> attributes = new List<SamlAttribute>();

            // create role claims
            List<string> roleList = new List<string>();
            roleList.Add("federatedServiceUser");
            roleList.Add("tokenRequestor");

            //TODO: Add all roles for the current ligged in user
            IIdentity p1 = ServiceSecurityContext.Current.PrimaryIdentity;

            attributes.Add(new SamlAttribute("http://schemas.microsoft.com/xsi/2005/05/role_meins", "role", roleList));



            //Create the SAML Assertion
            SamlAssertion assertion = new SamlAssertion();
            
            assertion.AssertionId = "_" + Guid.NewGuid().ToString();
            assertion.Issuer = "http://cloud.lollisoft.de";
            assertion.Statements.Add(new SamlAttributeStatement(subject, attributes));

			usersService usersService = new usersService();

            var users = usersService.Load();

            var user = users.FirstOrDefault(u => u._userid == p1.Name);

            var approvedAudiencesList = user.SecurityUsers_ApprovedAudiences_SecurityUsers;
            List<Uri> approvedAudiences = new List<Uri>();

            if (approvedAudiencesList != null)
            {
                foreach (var aa in approvedAudiencesList)
                {
                    var a = aa.ApprovedAudiences;

                    approvedAudiences.Add(new Uri(a._uri));
                }
            }

            // At least let the Management Service going for the builtin admin account
            if (p1.Name == "admin" && (approvedAudiencesList == null || !approvedAudiencesList.Any(a => a.ApprovedAudiences._uri == "net.tcp://cloud.lollisoft.de:49383/StsManagementService")))
            {
                if (!approvedAudiences.Contains(new Uri("net.tcp://cloud.lollisoft.de:49383/StsManagementService"))) 
					approvedAudiences.Add(new Uri("net.tcp://cloud.lollisoft.de:49383/StsManagementService"));
            }

			
            // At least let the Management Service going for the builtin admin account
            if (p1.Name == "admin" && (approvedAudiencesList == null || !approvedAudiencesList.Any(a => a.ApprovedAudiences._uri == "net.tcp://cloud.lollisoft.de:49381/StsManagementService")))
            {
                if (!approvedAudiences.Contains(new Uri("net.tcp://cloud.lollisoft.de:49381/StsManagementService"))) 
					approvedAudiences.Add(new Uri("net.tcp://cloud.lollisoft.de:49381/StsManagementService"));
            }
			
            // At least let the Management Service going for the builtin admin account (development port)
            if (p1.Name == "admin" && (approvedAudiencesList == null || !approvedAudiencesList.Any(a => a.ApprovedAudiences._uri == "net.tcp://cloud.lollisoft.de:49382/StsManagementService")))
            {
                if (!approvedAudiences.Contains(new Uri("net.tcp://cloud.lollisoft.de:49382/StsManagementService"))) 
					approvedAudiences.Add(new Uri("net.tcp://cloud.lollisoft.de:49382/StsManagementService"));
            }
			

            List<SamlCondition> conditions = new List<SamlCondition>();
            conditions.Add(new SamlAudienceRestrictionCondition(approvedAudiences));
            assertion.Conditions = new SamlConditions(DateTime.Now, DateTime.Now.AddMinutes(5), conditions);

            // Set the signing credentials for the SAML assertion
            string signatureAlgorithm = GetSignatureAlgorithm(signingKey);
            assertion.SigningCredentials = new SigningCredentials(signingKey, signatureAlgorithm, SecurityAlgorithms.Sha1Digest, signingKeyIdentifier);

            SamlSecurityToken token = new SamlSecurityToken(assertion);
            return token;
        }
    }
}
