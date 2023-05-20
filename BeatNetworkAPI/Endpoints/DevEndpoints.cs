using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.DevRequests;
using BeatNetworkAPI.Requests.UserRequests;

namespace BeatNetworkAPI.Endpoints;

public class DevEndpoints : IEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        //Figure out how to pass parameters here. You can use {param} in the URL, but thats not what we want!
        app.MediatePostAnon<GetTestGoogleAuthRequest>("/dev/auth");
    }

    public void DefineServices(IServiceCollection services)
    {
    }

}