using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.Repositories;
using BeatNetworkAPI.Requests.UserRequests;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using MediatR;
using FirebaseAuthException = Firebase.Auth.FirebaseAuthException;

namespace BeatNetworkAPI.Handlers.UserHandlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, IResult>
{
    private UserRepository _userRepository;
    private FirebaseAuth _firebaseAuth;

    public RegisterUserHandler(UserRepository userRepository, FirebaseAuth firebaseAuth)
    {
        _userRepository = userRepository;
        _firebaseAuth = firebaseAuth;
    }

    public async Task<IResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            //Check that the email doesn't already exist in FirebaseAuth
            try
            {
                var registeredUser = await _firebaseAuth.GetUserByEmailAsync(request.UserRegistrationDto.Email);
                if (registeredUser is not null) return Results.Problem("Email is already registered");
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                if (ex.ErrorCode != ErrorCode.NotFound)
                {
                    //Log it!
                    return Results.Problem("Auth error!");
                }
            }


            //Register the user in Identity Platform
            var userRecord = await _firebaseAuth.CreateUserAsync(new UserRecordArgs()
            {
                Email = request.UserRegistrationDto.Email,
                Password = request.UserRegistrationDto.Password
            });

            if (userRecord is null) return Results.Problem("Failed to register user");
            var uid = userRecord.Uid;

            var result = await _userRepository.AddUser(uid, request.UserRegistrationDto);
            if (result is not UserRegistrationResult.Success)
            {
                _firebaseAuth.DeleteUserAsync(uid);
                return Results.Problem("Failed to add user");
            }
            return Results.Ok(request.UserRegistrationDto.Username);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.ToString());
        }

        return Results.Ok(request.UserRegistrationDto.Username);
    }
}