using System.IdentityModel.Tokens.Jwt;
using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using MediatR;
using FirebaseAuthException = Firebase.Auth.FirebaseAuthException;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class RegisterSongHandler : IRequestHandler<InsertSongRequest, IResult>
{
    private SongRepository _songRepository;
    private UserRepository _userRepository;
    private IHttpContextAccessor _contextAccessor;

    public RegisterSongHandler(SongRepository songRepository, UserRepository userRepository, IHttpContextAccessor contextAccessor)
    {
        _songRepository = songRepository;
        _userRepository = userRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<IResult> Handle(InsertSongRequest request, CancellationToken cancellationToken)
    {
        var userClaim = _contextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userClaim is null) return Results.Problem("Could not resolve User ID");
        var userId = userClaim.Value;
        var user = await _userRepository.GetById(userId);
        if (user is null) return Results.Problem("Could not resolve User");
        var songDto = request.SongDto;
        songDto.User = new UserStubDTO()
        {
            Id = userId,
            Username = user.Username
        };
        return await _songRepository.Insert(request.SongDto) ? Results.Ok() : Results.Problem("Failed to insert song!");
    }
}