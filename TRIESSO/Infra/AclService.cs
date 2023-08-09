using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Infra
{
    public class AclService
    {

        readonly string urlBase = "https://win-nj6v2gg0m3q.trie.domain/adfs/ls/idpinitiatedsignonpage.aspx";
        static readonly HttpClient client = new();

        public async Task<string> GetSamlAuth(string samlRequestEncoded, HttpContext context)
        {
            var urlComplete = QueryHelpers.AddQueryString(urlBase, "SAMLRequest", samlRequestEncoded);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            
            var response = client.GetAsync(urlComplete).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return string.Format("{0}?SAMLRequest={1}", urlBase, HttpUtility.UrlEncode(samlRequestEncoded));
            }

            return null;
        }
    }
}
