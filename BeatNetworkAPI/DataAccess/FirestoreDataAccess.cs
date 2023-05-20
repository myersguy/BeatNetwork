using System.Runtime.InteropServices;
using Google.Cloud.Firestore;

namespace BeatNetworkAPI.DataAccess;

public class FirestoreDataAccess
{
    private FirestoreDb _db;

    public FirestoreDataAccess(FirestoreDb db)
    {
        _db = db;
    }

    public async Task<DocumentSnapshot?> GetSnapshotById(string collectionName, string id)
    {
        var collection = _db.Collection((collectionName));
        var snapshot = await collection.Document(id).GetSnapshotAsync();
        if (snapshot.Exists) return snapshot;

        return null;
    }

    public async Task<T> GetById<T>(string collectionName, string id) where T : class
    {
        {
            var collection = _db.Collection((collectionName));
            var snapshot = await collection.Document(id).GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<T>();
            }

            return null;
        }
    }

    public async Task<List<T>> GetByValue<T, TV>(string collectionName, string column, TV value)
    {
        var retval = new List<T>();
        var collection = _db.Collection(collectionName).WhereEqualTo(column, value);
        var snapshot = await collection.GetSnapshotAsync();
        foreach (var document in snapshot)
        {
            retval.Add(document.ConvertTo<T>());
        }

        return retval;
    }

    public async Task<bool> Insert<T>(string collectionName, T model)
    {
        var collection = _db.Collection(collectionName);
        return await Insert(collection, model);
    }

    public async Task<bool> Insert<T>(CollectionReference collection, T model)
    {
        try
        {
            var userWriteResult = await collection.Document().CreateAsync(model);
            if (userWriteResult is null)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> InsertOrReplace<T>(string collectionName, string id, T model)
    {
        var collection = _db.Collection(collectionName);
        return await InsertOrReplace(collection, id, model);
    }

    public async Task<bool> InsertOrReplace<T>(CollectionReference collection, string id, T model)
    {
        try
        {
            var userWriteResult = await collection.Document(id).SetAsync(model);
            if (userWriteResult is null)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> Update(CollectionReference collection, string id, Dictionary<string, object> updates)
    {
        try
        {
            var userWriteResult = await collection.Document(id).UpdateAsync(id, updates);
            if (userWriteResult is null)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}