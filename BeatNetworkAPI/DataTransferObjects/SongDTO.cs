using Domain.Models;
using Google.Cloud.Firestore;

namespace BeatNetwork.DataTransferObjects;

public class SongDTO : IDto
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Host { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public long CommentCount { get; set; }
    public DateTime Created { get; set; }
    public UserStubDTO User { get; set; }

    public static explicit operator SongDTO(Song song)
    {
        return new SongDTO()
        {
            Id = song.Reference.Id,
            Url = song.Url,
            Host = song.Host,
            Title = song.Title,
            Description = song.Description,
            Created = song.Created.ToDateTime(),
            CommentCount = song.CommentCount,
            User = (UserStubDTO)song.User,
        };
    }
}

public class SongStubDTO : IDto
{
    public string Id { get; set; }
    public string Title { get; set; }

    public static explicit operator SongStubDTO(SongStub song)
    {
        return new SongStubDTO()
        {
            Id = song.Id,
            Title = song.Title
        };
    }
}