using Microsoft.AspNetCore.Mvc;

namespace BeatNetworkAPI.Requests.DevRequests;

public class GetTestGoogleAuthRequest : IHttpRequest
{
    [FromBody] public string user { get; set; }
    [FromBody] public string password { get; set; }
}