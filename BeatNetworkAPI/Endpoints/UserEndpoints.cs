using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.UserRequests;

namespace BeatNetworkAPI.Endpoints;

public class UserEndpoints : IEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MediateGet<GetUserByIdRequest>("/users/{id}");
        app.MediatePostAnon<RegisterUserRequest>("/users/register");
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<UserRepository>();
    }

}