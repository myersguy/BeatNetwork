using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;
using MediatR;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class GetSongCommentsBySongIdHandler : IRequestHandler<GetSongCommentsBySongIdRequest, IResult>
{
    private SongCommentRepository _songCommentRepository;

    public GetSongCommentsBySongIdHandler(SongCommentRepository songCommentRepository)
    {
        _songCommentRepository = songCommentRepository;
    }

    public async Task<IResult> Handle(GetSongCommentsBySongIdRequest request, CancellationToken cancellationToken)
    {
        var songs = await _songCommentRepository.GetBySongId(request.Id);
        return Results.Ok(songs.Select(x => (SongCommentDTO)x));
    }
}