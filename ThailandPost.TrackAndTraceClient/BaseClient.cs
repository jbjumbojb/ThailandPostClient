using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ThailandPost.ThailandPostClient
{
    public class BaseClient
    {
        protected string endpoint;
        protected HttpMessageHandler messageHandler;
        public BaseClient(HttpMessageHandler messageHandler, string endpoint)
        {
            this.messageHandler = messageHandler;
            this.endpoint = endpoint;
        }
        protected HttpClient GetHttpClient(string token)
        {
            HttpClient httpClient;
            if (messageHandler != null)
                httpClient = new HttpClient(messageHandler);
            else httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint, UriKind.Absolute);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
            return httpClient;
        }
        protected static StringContent ObjectToStringContent(object obj)
        {
            return new StringContent(
                JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                Encoding.UTF8,
                "application/json"
                );
        }
    }
}
