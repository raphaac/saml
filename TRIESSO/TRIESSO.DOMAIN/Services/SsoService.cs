using Infra;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using TrieSSO.Domain.Models;

namespace TrieSSO.Domain
{
    public class SsoService
    {
        public void GetSamlAuthRequest() {

            var aclService = new AclService();
            aclService.GetSamlAuth();
        }

        protected string MakeSamlRequest() 
        {
            var samlRequest = new SamlRequest(
                    "_" + Guid.NewGuid().ToString(),
                    DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                    "https://www.dropbox.com/saml_login",
                    "Dropbox");

            return GenerateSamlRequestCoded(samlRequest);
        }

        protected static string GenerateSamlRequestCoded(SamlRequest valuesSaml)
        {

            using StringWriter stringWriter = new();
            XmlWriterSettings xmlWriterSettings = new() { OmitXmlDeclaration = true };

            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
            {
                xmlWriter.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                xmlWriter.WriteAttributeString("ID", valuesSaml.Id);
                xmlWriter.WriteAttributeString("Version", "2.0");
                xmlWriter.WriteAttributeString("IssueInstant", valuesSaml.IssueInstant);
                xmlWriter.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
                xmlWriter.WriteAttributeString("AssertionConsumerServiceURL", valuesSaml.AssertionConsumerServiceEndPoint);

                xmlWriter.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                xmlWriter.WriteString(valuesSaml.RelyingPartyIdentifier);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
                xmlWriter.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified");
                xmlWriter.WriteAttributeString("AllowCreate", "true");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }

            return ConvertToBase64Deflated(stringWriter.ToString());
        }

		protected static string ConvertToBase64Deflated(string input)
		{
			var memoryStream = new MemoryStream();
			
			using (var writer = new StreamWriter(new DeflateStream(memoryStream, CompressionMode.Compress, true), new UTF8Encoding(false)))
			{
				writer.Write(input);
				writer.Close();
			}
			
			string result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.None);
			
			return result;
		}
	}
}
