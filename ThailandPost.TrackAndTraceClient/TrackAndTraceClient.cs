﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThailandPost.ThailandPostClient;

namespace ThailandPost
{
    public class TrackAndTraceClient : BaseClient
    {
        public TrackAndTraceClient() : this("https://trackapi.thailandpost.co.th/post/api/v1/")
        {
        }
        public TrackAndTraceClient(string endpoint) : base(null, endpoint)
        {
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

        public async Task<ResponseResult<GetItemsResponse>> GetItemsAsync(string accessToken, IEnumerable<string> trackingNumber, Language language = Language.TH, Status status = Status.All)
        {
            var response = await GetHttpClient(accessToken).PostAsync("track", ObjectToStringContent(new GetItemsRequest
            {
                Barcode = trackingNumber,
                Language = language.ToString(),
                Status = status == 0 ? "all" : ((int)status).ToString()
            }));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ResponseResult<GetItemsResponse>>((await response.Content.ReadAsStringAsync()));
        }
    }
}
