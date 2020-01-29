using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ThailandPost.TrackAndTraceClient
{
    public class TrackAndTraceClient
    {
        private readonly string endpoint;
        private readonly HttpMessageHandler messageHandler;
        public TrackAndTraceClient() : this("https://trackapi.thailandpost.co.th/post/api/v1/")
        {
        }
        public TrackAndTraceClient(string endpoint) : this(null, endpoint)
        {
        }

        public TrackAndTraceClient(HttpMessageHandler messageHandler, string endpoint)
        {
            this.messageHandler = messageHandler;
            this.endpoint = endpoint;
        }

        private HttpClient GetHttpClient(string token)
        {
            HttpClient httpClient;
            if (messageHandler != null)
                httpClient = new HttpClient(messageHandler);
            else httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint, UriKind.Absolute);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
            return httpClient;
        }
        public async Task<GetTokenResponse> GetAccessTokenAsync(string appToken)
        {
            var response = await GetHttpClient(appToken).PostAsync("authenticate/token", new StringContent(string.Empty));
            var definition = new { expire = "", token = "" };
            var responseJson = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), definition);
            return new GetTokenResponse
            {
                Expire = DateTimeOffset.TryParseExact(responseJson.expire, "yyyy-MM-dd HH:mm:sszzz", System.Globalization.CultureInfo.GetCultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out DateTimeOffset expire) ? expire : (DateTimeOffset?)null,
                Token = responseJson.token
            };
        }

        public async Task<ResponseResult<GetItemsResponse>> GetItemsAsync(string accessToken, IEnumerable<string> items, Language language = Language.TH, Status status = Status.All)
        {
            var response = await GetHttpClient(accessToken).PostAsync("track", ObjectToStringContent(new GetItemsRequest
            {
                Barcode = items,
                Language = language.ToString(),
                Status = status == 0 ? "all" : ((int)status).ToString()
            }));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ResponseResult<GetItemsResponse>>((await response.Content.ReadAsStringAsync()));
        }

        private static StringContent ObjectToStringContent(object obj)
        {
            return new StringContent(
                JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                Encoding.UTF8,
                "application/json"
                );
        }
    }
}
