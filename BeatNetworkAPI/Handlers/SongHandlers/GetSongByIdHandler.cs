using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.SongRequests;
using BeatNetworkAPI.Requests.UserRequests;
using MediatR;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class GetSongByIdHandler : IRequestHandler<GetSongByIdRequest, IResult>
{
    private SongRepository _songRepository;

    public GetSongByIdHandler(SongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<IResult> Handle(GetSongByIdRequest request, CancellationToken cancellationToken)
    {
        var song = await _songRepository.GetById(request.Id);
        if (song is null) return Results.NotFound();
        return Results.Ok((SongDTO)song);
    }
}