
using Google.Api.Gax;
using Google.Cloud.Firestore;
using PeakForm.Model;
using System.Collections.ObjectModel;
namespace PeakForm.Services;

class FireStoreServices {


    private FirestoreDb db;

    public ObservableCollection<Quests> QuestItems { get; private set; }
    public ObservableCollection<Penalty> PenaltyItems { get; private set; }
    private readonly INavigation _navigationFireServices;
    public FireStoreServices(INavigation navigation) {
        _navigationFireServices = navigation;
    } 
    private async Task SetUpFireStore() {
        QuestItems = new ObservableCollection<Quests>();
        Penalty = new ObservableCollection<Penalty>();
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

    public async void RetrieveQuestData()
    {
        Query allItemsQuery = db.Collection("Quest");  // Replace with your collection name
        QuerySnapshot snapshot = await allItemsQuery.GetSnapshotAsync();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            if (document.Exists)
            {
                Quests qs = document.ConvertTo<Quests>();
                QuestItems.Add(qs);
            }
        }
    }
    public async void RetrievePenaltiesData() {
        Query allItemQuery = db.Collection("PenaltyItems");
        QuerySnapshot snapshot = await allItemQuery.GetSnapshotAsync();   
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            if (document.Exists)
            {
                
                    // Map Firestore data to your PenaltyItem model
                    var penaltyItem = new Penalty
                    {
                        PenaltyItems = document.GetString("PenaltyItems")
                    };

                    // Add each item to the ObservableCollection
                    PenaltyItems.Add(penaltyItem);
                
            }
        }

    }

    public async Task DeleteDocumentAsync(string collectionName, string documentId)
    {
        DocumentReference docRef = db.Collection(collectionName).Document(documentId);
        await docRef.DeleteAsync();
       
    }
}


