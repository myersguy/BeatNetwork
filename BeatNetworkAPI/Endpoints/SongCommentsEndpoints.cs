using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;

namespace BeatNetworkAPI.Endpoints;

public class SongCommentEndpoints : IEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MediateGet<GetSongCommentsBySongIdRequest>("/comments/{id}");
        app.MediateGet<GetSongCommentsByUserIdRequest>("/comments/user/{id}");
        app.MediatePost<InsertSongCommentRequest>("/comments/{songid}");
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<SongCommentRepository>();
        services.AddSingleton<SongRepository>();
    }

}