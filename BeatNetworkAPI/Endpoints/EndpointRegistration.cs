using BeatNetworkAPI.Endpoints;

public static class EndpointRegistration
{
    //Theres a sexy way to handle this shit with reflection, but I sure don't know it
    //TODO: OR, Could this be a partial class? Seems like it might work to me.
    public static void RegisterBeatNetworkEndpoints(this WebApplication app)
    {
        var userEndpoints = new UserEndpoints();
        userEndpoints.DefineEndpoints(app);
        var songEndpoints = new SongEndpoints();
        songEndpoints.DefineEndpoints(app);
        var songCommentEndpoints = new SongCommentEndpoints();
        songCommentEndpoints.DefineEndpoints(app);
        if (app.Environment.IsDevelopment())
        {
            var devEndpoints = new DevEndpoints();
            devEndpoints.DefineEndpoints(app);
        }
    }

    public static void RegisterBeatNetworkServices(this IServiceCollection services)
    {
        var userEndpoints = new UserEndpoints();
        userEndpoints.DefineServices(services);
        var songEndpoints = new SongEndpoints();
        songEndpoints.DefineServices(services);
        var songCommentEndpoints = new SongCommentEndpoints();
        songCommentEndpoints.DefineServices(services);
    }
}