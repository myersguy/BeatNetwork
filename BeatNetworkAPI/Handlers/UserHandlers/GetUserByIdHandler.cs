using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.UserRequests;
using MediatR;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, IResult>
{
    private UserRepository _userRepository;

    public GetUserByIdHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);
        if (user is null) return Results.NotFound();
        return Results.Ok((UserDTO)user);
    }
}