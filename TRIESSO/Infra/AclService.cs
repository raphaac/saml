using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Infra
{
    public class AclService
    {

        static HttpClient client = new HttpClient();

        public async Task<string> GetSamlAuth(string samlRequestEncoded, HttpContext context)
        {
            var urlBase = "https://win-nj6v2gg0m3q.trie.domain/adfs/ls/idpinitiatedsignonpage.aspx";
            var urlComplete = QueryHelpers.AddQueryString(urlBase, "SAMLRequest", samlRequestEncoded);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            
            var response = client.GetAsync(urlComplete).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return string.Format("https://win-nj6v2gg0m3q.trie.domain/adfs/ls/idpinitiatedsignon.aspx?SAMLRequest={0}", HttpUtility.UrlEncode(samlRequestEncoded));
            }

            return null;
        }
    }
}
