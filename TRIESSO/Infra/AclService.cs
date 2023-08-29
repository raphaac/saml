using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
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

        public async Task<string> PostSamlLogout(string samlLogout)
        {
            samlLogout = "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";

            var urlBaseLogout = "https://win-nj6v2gg0m3q.trie.domain/adfs/ls/?wa=wsignout1.0";

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            var dict = new Dictionary<string, string>();
            dict.Add("SAMLRequest", samlLogout);            

            using var req = new HttpRequestMessage(HttpMethod.Post, urlBaseLogout) { Content = new FormUrlEncodedContent(dict) };
            using var response = await client.SendAsync(req);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();                
            }

            return null;
        }
    }
}
