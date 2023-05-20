using Google.Cloud.Firestore;

namespace Domain.Models;

[FirestoreData]
public class User
{
    [FirestoreDocumentId] public DocumentReference Reference { get; set; }
    [FirestoreDocumentCreateTimestamp] public Timestamp Created { get; set; }

    [FirestoreDocumentUpdateTimestamp] public Timestamp Updated { get; set; }
    [FirestoreProperty] public string Username { get; set; }
    [FirestoreProperty] public string Email { get; set; }

    [FirestoreProperty] public Timestamp LastLogin { get; set; }
    [FirestoreProperty] public long Points { get; set; }
    
    [FirestoreProperty] public long CommentCount { get; set; }
}

[FirestoreData]
public class UserStub
{
    [FirestoreProperty] public string Id { get; set; }
    [FirestoreProperty] public string Username { get; set; }
}