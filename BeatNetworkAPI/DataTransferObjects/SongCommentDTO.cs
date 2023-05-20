using Domain.Models;

namespace BeatNetwork.DataTransferObjects;

public class SongCommentDTO : IDto
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public UserStubDTO User { get; set; }
    public SongStubDTO Song { get; set; }
    public string Comment { get; set; }
    public bool Deleted { get; set; }

    public static explicit operator SongCommentDTO(SongComment songComment)
    {
        return new SongCommentDTO()
        {
            Id = songComment.Reference.Id,
            Created = songComment.Created.ToDateTime(),
            User = (UserStubDTO)songComment.User,
            Song = (SongStubDTO)songComment.Song,
            Comment = songComment.Comment,
            Deleted = songComment.Deleted,
        };
    }
}