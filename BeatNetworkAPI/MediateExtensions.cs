using BeatNetworkAPI.Requests;
using MediatR;

namespace BeatNetworkAPI;

public static class MediateExtensions
{
    public static WebApplication MediateGet<TRequest>(
        this WebApplication app,
        string template) where TRequest : IHttpRequest
    {
        app.MapGet(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await mediator.Send(request));
        return app;
    }

    public static WebApplication MediatePost<TRequest>(
        this WebApplication app,
        string template) where TRequest : IHttpRequest
    {
        app.MapPost(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await mediator.Send(request));
        return app;
    }

    public static WebApplication MediatePut<TRequest>(
        this WebApplication app,
        string template) where TRequest : IHttpRequest
    {
        app.MapPut(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await mediator.Send(request));
        return app;
    }

    public static WebApplication MediateGetAnon<TRequest>(
        this WebApplication app,
        string template) where TRequest : IHttpRequest
    {
        app.MapGet(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await mediator.Send(request)).AllowAnonymous();
        return app;
    }

    public static WebApplication MediatePostAnon<TRequest>(
        this WebApplication app,
        string template) where TRequest : IHttpRequest
    {
        app.MapPost(template,
            async (IMediator mediator, TRequest request) =>
                await mediator.Send(request)).AllowAnonymous();
        return app;
    }
}