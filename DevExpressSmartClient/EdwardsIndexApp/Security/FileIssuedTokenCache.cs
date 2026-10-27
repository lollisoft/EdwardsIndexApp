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
    // A simple file-based durable cache for caching issued tokens at a wider scope than the channel 
    // and that can survive app recycles.
    // The file needs to be ACL'd strongly so as not to reveal the issued token key.
    // A real cache implementation will have quotas, eviction policies etc.
    public class FileIssuedTokenCache : IssuedTokenCacheBase
    {

        string fileName;

        public FileIssuedTokenCache(string fileName)
            : base()
        {
            this.fileName = fileName;
            if (File.Exists(fileName))
            {
                using (FileStream str = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    str.Seek(0, SeekOrigin.Begin);
                    PopulateCache(this.Cache, str);
                }
            }
        }

        protected override void OnTokenAdded(Key key, GenericXmlSecurityToken token)
        {
            using (FileStream str = File.Open(fileName, FileMode.Append, FileAccess.Write))
            {
                str.Seek(0, SeekOrigin.End);
                SerializeCachedToken(str, key.Target, key.Issuer, token);
            }
        }

        protected override void OnTokenRemoved(Key key)
        {
            // just persist all token entries back into the file
            using (FileStream str = File.Open(fileName, FileMode.Append, FileAccess.Write))
            {
                str.Seek(0, SeekOrigin.Begin);
                foreach (Key k in this.Cache.Keys)
                {
                    GenericXmlSecurityToken token = this.Cache[k];
                    SerializeCachedToken(str, key.Target, key.Issuer, token);
                }
            }
        }

        #region token persistence methods
        // Since the issued token is an endorsing token it will not be used for identity checks
        // hence we do not need to persist the authorization policies that represent the target service.
        static void SerializeCachedToken(Stream stream, Uri target, Uri issuer, GenericXmlSecurityToken token)
        {
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            writer.WriteStartElement("Entry");
            writer.WriteElementString("Target", target.AbsoluteUri);
            writer.WriteElementString("Issuer", (issuer == null) ? "" : issuer.AbsoluteUri);

            writer.WriteStartElement("Token");
            writer.WriteStartElement("XML");
            token.TokenXml.WriteTo(writer);
            writer.WriteEndElement();
            SymmetricSecurityKey key = (SymmetricSecurityKey)(token.SecurityKeys[0]);
            writer.WriteElementString("Key", Convert.ToBase64String(key.GetSymmetricKey()));
            writer.WriteElementString("Id", token.Id);
            writer.WriteElementString("ValidFrom", Convert.ToString(token.ValidFrom));
            writer.WriteElementString("ValidTo", Convert.ToString(token.ValidTo));
            WSSecurityTokenSerializer serializer = new WSSecurityTokenSerializer();
            writer.WriteStartElement("InternalTokenReference");
            serializer.WriteKeyIdentifierClause(writer, token.InternalTokenReference);
            writer.WriteEndElement();
            writer.WriteStartElement("ExternalTokenReference");
            serializer.WriteKeyIdentifierClause(writer, token.ExternalTokenReference);
            writer.WriteEndElement();

            writer.WriteEndElement(); // token
            writer.WriteEndElement(); // entry

            writer.Flush();
            stream.Flush();
        }

        static void PopulateCache(Dictionary<Key, GenericXmlSecurityToken> cache, Stream stream)
        {
            XmlTextReader reader = new XmlTextReader(stream);
            while (reader.IsStartElement("Entry"))
            {
                reader.ReadStartElement();
                Uri target = new Uri(reader.ReadElementString("Target"));
                string issuerStr = reader.ReadElementString("Issuer");
                Uri issuer = string.IsNullOrEmpty(issuerStr) ? null : new Uri(issuerStr);

                reader.ReadStartElement("Token");
                reader.ReadStartElement("XML");
                XmlDocument doc = new XmlDocument();
                XmlElement tokenXml = doc.ReadNode(reader) as XmlElement;
                reader.ReadEndElement();
                byte[] key = Convert.FromBase64String(reader.ReadElementString("Key"));
                reader.ReadElementString("Id");
                DateTime validFrom = Convert.ToDateTime(reader.ReadElementString("ValidFrom"));
                DateTime validTo = Convert.ToDateTime(reader.ReadElementString("ValidTo"));
                WSSecurityTokenSerializer serializer = new WSSecurityTokenSerializer();
                reader.ReadStartElement("InternalTokenReference");
                SecurityKeyIdentifierClause internalReference = serializer.ReadKeyIdentifierClause(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("ExternalTokenReference");
                SecurityKeyIdentifierClause externalReference = serializer.ReadKeyIdentifierClause(reader);
                reader.ReadEndElement();
                reader.ReadEndElement(); // token
                reader.ReadEndElement(); // entry

                List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
                GenericXmlSecurityToken token = new GenericXmlSecurityToken(tokenXml, new BinarySecretSecurityToken(key), validFrom, validTo, internalReference, externalReference,
                    new ReadOnlyCollection<IAuthorizationPolicy>(policies));
                cache.Add(new Key(target, issuer), token);
            }
        }
        #endregion
    }
}
