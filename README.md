# ThailandPostClient

## Prerequisite
- ThailandPost *AppToken* ([register here](https://track.thailandpost.co.th/))
## Installation
```
PM> Install-Package ThailandPostClient
```
## Usage

```csharp
using ThailandPost.TrackAndTraceClient;

var trackingClient = new TrackAndTraceClient();
GetTokenResponse accessToken = trackingClient.GetAccessTokenAsync(**AppToken**);
//save token for future use
var tokenExpire = accessToken.Expire; //check expire
var token = accessToken.Token;

ResponseResult<GetItemsResponse> result = await trackingClient.GetItemsAsync(token, new string[]{"EY145587896TH","RC338848854TH"},ThailandPost.Language.TH);

if(result.Status){
    foreach(var item in Keyresult.Response.Items){
        Console.WriteLine($"{item.Key}:");
        foreach(var status in item)
            Console.WriteLine($"{status.StatusDescription} - {status.StatusDate}")
    }
    /* 
        EY145587896TH:
        อยู่ระหว่างการขนส่ง - 20/01/2563 18:07:00+07:00
        นำจ่ายสำเร็จ - 21/01/2563 12:55:35+07:00
    */
}else{
    Console.WriteLine(result.Message);
}
```
The main documentation is [ThailandPost API Document](https://track.thailandpost.co.th/developerGuide)
