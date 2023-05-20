using BeatNetwork.DataTransferObjects;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeatNetworkAPI.Requests.UserRequests;

public class RegisterUserRequest : IHttpRequest
{
    [FromBody] public UserRegistrationDTO UserRegistrationDto { get; set; }
}