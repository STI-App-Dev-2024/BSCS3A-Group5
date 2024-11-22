
using Google.Api.Gax;
using Google.Cloud.Firestore;
using PeakForm.Model;
namespace PeakForm.Services;

class FireStoreServices{


    private FirestoreDb db;
    private async Task SetUpFireStore() {
        if (db == null) {
            var stream = await FileSystem.OpenAppPackageFileAsync("admin-sdk.json");
            var reader = new StreamReader(stream);  
            var contents = reader.ReadToEnd();
            db = new FirestoreDbBuilder
            {
                ProjectId = "peakform-4c94a",
                ConverterRegistry = new ConverterRegistry
                {
                   new DateTimeToTimeStampConverter(),
                },
                JsonCredentials = contents
            }.Build();
        }
    
    }

    public async Task CreateAccount(UserAccount useraccount) {
        await SetUpFireStore();
        await db.Collection("UserAccount").AddAsync(useraccount);
    }

    public async Task<T?> GetDocumentAsync<T>(string collectionName, string documentId) where T : class
    {
        DocumentReference docRef = db.Collection(collectionName).Document(documentId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            T data = snapshot.ConvertTo<T>();
            return data;
        }
  
        return null;
    }

    public async Task DeleteDocumentAsync(string collectionName, string documentId)
    {
        DocumentReference docRef = db.Collection(collectionName).Document(documentId);
        await docRef.DeleteAsync();
       
    }
}


