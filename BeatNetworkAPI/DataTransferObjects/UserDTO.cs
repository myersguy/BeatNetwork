using Domain.Models;
using Google.Cloud.Firestore;

namespace BeatNetwork.DataTransferObjects;

public class UserDTO : IDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public DateTime Created { get; set; }
    //public DateTime LastLogin { get; set; }
    public long Points { get; set; }
    public long CommentCount { get; set; }

    public static explicit operator UserDTO(User user)
    {
        return new UserDTO()
        {
            Id = user.Reference.Id,
            Username = user.Username,
            Created = user.Created.ToDateTime(),
           // LastLogin = user.LastLogin,
            Points = user.Points,
            CommentCount = user.CommentCount
        };
    }
}

public class UserStubDTO : IDto
{
    public string Id { get; set; }
    public string Username { get; set; }

    public static explicit operator UserStubDTO(UserStub user)
    {
        return new UserStubDTO()
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}
