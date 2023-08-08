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

        public async void GetSamlAuth(string samlRequestEncoded)
        {
            var urlBase = "https://win-nj6v2gg0m3q.trie.domain/adfs/ls";
            var urlComplete = QueryHelpers.AddQueryString(urlBase, "SAMLRequest", samlRequestEncoded);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            var response = client.GetAsync(urlComplete).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                //TODO: Alterar a URL Base para que esta contenha a página aspx
                //verificar como recuperar e armazenar o cookie para envia-lo na próxima requisição
            }

        }
    }
}
