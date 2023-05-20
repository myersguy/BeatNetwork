using System.Reflection.Metadata.Ecma335;
using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.DataAccess;
using Domain.Models;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.IdentityModel.Tokens;

namespace BeatNetworkAPI.Repositories;

public class SongRepository
{
    private FirestoreDataAccess _db;
    
    public SongRepository(FirestoreDataAccess db)
    {
        _db = db;
    }

    public async Task<Song?> GetById(string id)
    {
        return await _db.GetById<Song?>("songs", id);
    }

    public  async Task<bool> Insert(SongDTO dto)
    {
        try
        {
            var song = new Song()
            {
                Title = dto.Title,
                Description = dto.Description,
                Host = dto.Host,
                Url = dto.Url,
                User = new UserStub()
                {
                    Id = dto.User.Id,
                    Username = dto.User.Username
                }
            };
            _db.Insert("songs", song);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}