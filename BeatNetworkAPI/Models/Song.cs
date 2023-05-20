using Google.Cloud.Firestore;

namespace Domain.Models;

[FirestoreData]
public class Song
{
    [FirestoreDocumentId] public DocumentReference Reference { get; set; }
    [FirestoreDocumentCreateTimestamp] public Timestamp Created { get; set; }

    [FirestoreDocumentUpdateTimestamp] public Timestamp Updated { get; set; }
    [FirestoreProperty] public UserStub User { get; set; }

    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string Url { get; set; }
    [FirestoreProperty] public string Host { get; set; }
    [FirestoreProperty] public string Description { get; set; }
    
    [FirestoreProperty] public long CommentCount { get; set; }
}

[FirestoreData]
public class SongStub
{
    [FirestoreProperty] public string Id { get; set; }
    [FirestoreProperty] public string Title { get; set; }
}