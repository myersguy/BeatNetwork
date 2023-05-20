using BeatNetwork.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace BeatNetworkAPI.Requests.SongRequests;

public class InsertSongRequest : IHttpRequest
{
    [FromBody] public SongDTO SongDto { get; set; }
}