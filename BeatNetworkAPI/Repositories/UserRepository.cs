using System.Reflection.Metadata.Ecma335;
using BeatNetwork.DataTransferObjects;
using BeatNetworkAPI.DataAccess;
using Domain.Models;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.IdentityModel.Tokens;

namespace BeatNetworkAPI.Repositories;

public class UserRepository
{
    private FirestoreDataAccess _db;

    public UserRepository(FirestoreDataAccess db)
    {
        _db = db;
    }

    public async Task<User?> GetById(string id)
    {
        return await _db.GetById<User?>("users", id);
    }

    public async Task<UserRegistrationResult> AddUser(string uid, UserRegistrationDTO dto)
    {
        try
        {
            //Check that the username isn't taken.
            var usernameSnapshot = await _db.GetSnapshotById("usernames", dto.Username.ToLower());
            if (usernameSnapshot is not null && usernameSnapshot.Exists)
            {
                return UserRegistrationResult.UsernameExists; //Def need better handling for error codes, etc
            }

            //Create the username collection entry
            var usernameInsert = await _db.Insert("usernames", new { uid = uid });
            if (usernameInsert is false)
            {
                return UserRegistrationResult.Error;
            }

            //Create the userdata
            var user = new User()
            {
                Email = dto.Email,
                Username = dto.Username,
                Points = 10
            };

            var userWriteResult = await _db.InsertOrReplace("users", uid, user);
            if (userWriteResult is false)
            {
                FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
                //Need to delete the entry from usernames TODO TODO TODO
                return UserRegistrationResult.Error;
            }

            return UserRegistrationResult.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return UserRegistrationResult.Error;
        }
    }

    public struct RegistrationResult
    {
        public UserRegistrationResult Result { get; set; }
        public string Exception { get; set; }
    }
}