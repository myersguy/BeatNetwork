using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;

namespace BeatNetworkAPI.Endpoints;

public class SongEndpoints : IEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MediateGet<GetSongByIdRequest>("/songs/{id}");
        app.MediatePost<InsertSongRequest>("/songs");
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<SongRepository>();
    }

}