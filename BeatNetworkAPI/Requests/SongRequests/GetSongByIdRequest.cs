namespace BeatNetworkAPI.Requests.SongRequests;

public class GetSongByIdRequest : IHttpRequest
{
    public string Id { get; set; }
}