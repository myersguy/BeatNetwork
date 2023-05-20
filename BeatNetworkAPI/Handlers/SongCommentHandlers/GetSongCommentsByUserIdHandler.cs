using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;
using MediatR;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class GetSongCommentsByUserIdHandler : IRequestHandler<GetSongCommentsByUserIdRequest, IResult>
{
    private SongCommentRepository _songCommentRepository;

    public GetSongCommentsByUserIdHandler(SongCommentRepository songCommentRepository)
    {
        _songCommentRepository = songCommentRepository;
    }
    
    public async Task<IResult> Handle(GetSongCommentsByUserIdRequest request, CancellationToken cancellationToken)
    {
        var songs = await _songCommentRepository.GetByUserId(request.Id);
        return Results.Ok(songs.Select(x => (SongCommentDTO)x));
    }
}