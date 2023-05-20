namespace BeatNetworkAPI.Requests.UserRequests;

public class GetUserByIdRequest : IHttpRequest
{
    public string Id { get; set; }
}