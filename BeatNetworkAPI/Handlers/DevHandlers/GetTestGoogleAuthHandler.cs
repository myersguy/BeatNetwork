using BeatNetworkAPI.Requests.DevRequests;
using BeatNetworkAPI.Requests.UserRequests;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using MediatR;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class GetTestGoogleAuthHandler : IRequestHandler<GetTestGoogleAuthRequest, IResult>
{
    private FirebaseAuthClient _firebaseAuthClient;

    public GetTestGoogleAuthHandler(FirebaseAuthClient firebaseAuthClient)
    {
        _firebaseAuthClient = firebaseAuthClient;
    }

    public async Task<IResult> Handle(GetTestGoogleAuthRequest request, CancellationToken cancellationToken)
    {
        if (request.user is null || request.password is null) return Results.BadRequest("Must pass user/password");
        var credential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(request.user, request.password);
        var token = credential.User.Credential.IdToken;
        if (token is null) return Results.NotFound("No token found");
        return Results.Ok(token);
    }
}