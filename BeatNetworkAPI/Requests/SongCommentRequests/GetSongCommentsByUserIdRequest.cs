namespace BeatNetworkAPI.Requests.SongRequests;

public class GetSongCommentsByUserIdRequest : IHttpRequest
{
    public string Id { get; set; }
}