using System.Reflection.Metadata.Ecma335;
using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.DataAccess;
using Domain.Models;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.IdentityModel.Tokens;

namespace BeatNetworkAPI.Repositories;

public class SongCommentRepository
{
    private FirestoreDataAccess _db;

    public SongCommentRepository(FirestoreDataAccess db)
    {
        _db = db;
    }

    public async Task<List<SongComment>> GetBySongId(string id)
    {
        return await _db.GetByValue<SongComment, string>("songcomments", "Song.Id", id);
    }

    public async Task<List<SongComment>> GetByUserId(string id)
    {
        return await _db.GetByValue<SongComment, string>("songcomments", "User.Id", id);
    }

    public async Task<bool> Insert(SongCommentDTO dto)
    {
        var songComment = new SongComment()
        {
            Comment = dto.Comment,
            Song = new SongStub()
            {
                Id = dto.Song.Id,
                Title = dto.Song.Title
            },
            User = new UserStub()
            {
                Id = dto.User.Id,
                Username = dto.User.Username
            }
        };
        return await _db.Insert("songcomments", songComment);
    }
}