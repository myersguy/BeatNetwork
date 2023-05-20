using Google.Cloud.Firestore;

namespace Domain.Models;

[FirestoreData]
public class SongComment
{
    [FirestoreDocumentId] public DocumentReference Reference { get; set; }
    [FirestoreDocumentCreateTimestamp] public Timestamp Created { get; set; }
    
    [FirestoreProperty] public SongStub Song { get; set; }
    [FirestoreProperty] public UserStub User { get; set; }
    [FirestoreProperty] public string Comment { get; set; }
    [FirestoreProperty] public bool Deleted { get; set; }
}
