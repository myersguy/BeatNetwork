namespace BeatNetwork.DataTransferObjects;

public class UserRegistrationDTO : IDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}