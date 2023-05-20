using System.IdentityModel.Tokens.Jwt;
using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using MediatR;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class InsertSongCommentHandler : IRequestHandler<InsertSongCommentRequest, IResult>
{
    private SongCommentRepository _songCommentRepository;
    private SongRepository _songRepository;
    private UserRepository _userRepository;
    private IHttpContextAccessor _contextAccessor;

    public InsertSongCommentHandler(SongCommentRepository songCommentRepository, SongRepository songRepository, UserRepository userRepository, IHttpContextAccessor contextAccessor)
    {
        _songCommentRepository = songCommentRepository;
        _songRepository = songRepository;
        _userRepository = userRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<IResult> Handle(InsertSongCommentRequest request, CancellationToken cancellationToken)
    {
        var userid = _contextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userid is null) return Results.Problem("Could not resolve User ID");
        var user = await _userRepository.GetById(userid.Value);
        if (user is null) return Results.Problem("Could not resolve User");

        var songId = request.SongId;
        var song = await _songRepository.GetById(songId);
        if (song is null) return Results.Problem("Unable to resolve Song ID");
        
        var songCommentDto = request.SongCommentDto;
        songCommentDto.User = new UserStubDTO()
        {
            Id = userid.Value,
            Username = user.Username
        };
        songCommentDto.Song = new SongStubDTO()
        {
            Id = songId,
            Title = song.Title
        };
        return await _songCommentRepository.Insert(songCommentDto) ? Results.Ok() : Results.Problem("Failed to insert song comment!");
    }
}