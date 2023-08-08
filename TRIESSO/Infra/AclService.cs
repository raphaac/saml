using Flurl.Http.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Infra
{
    public class AclService
    {

        static HttpClient client = new HttpClient();

        public async void GetSamlAuth()
        {
            //var urlBase = "https://win-nj6v2gg0m3q.trie.domain/adfs/ls";
            var urlBase = "https://jsonplaceholder.typicode.com/todos/1";

            //var urlComplete = QueryHelpers.AddQueryString(urlBase, "SAMLRequest", "sdfadfd");
            var urlComplete = urlBase;

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7"));

            var response = client.GetAsync(urlComplete).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
            }

        }
    }
}
