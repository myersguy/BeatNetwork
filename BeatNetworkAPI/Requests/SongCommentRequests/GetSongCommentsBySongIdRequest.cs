namespace BeatNetworkAPI.Requests.SongRequests;

public class GetSongCommentsBySongIdRequest : IHttpRequest
{
    public string Id { get; set; }
}