using BeatNetwork.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace BeatNetworkAPI.Requests.SongRequests;

public class InsertSongCommentRequest : IHttpRequest
{
    public string SongId { get; set; }
    [FromBody] public SongCommentDTO SongCommentDto { get; set; }
}