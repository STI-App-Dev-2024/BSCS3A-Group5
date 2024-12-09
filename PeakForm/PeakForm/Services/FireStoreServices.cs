
using Firebase.Auth;
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
        if (useraccount == null) throw new ArgumentNullException(nameof(useraccount));
        await SetUpFireStore();
        string email = useraccount.Email;
        DocumentReference userdoc = db.Collection("UserAccount").Document(email);
        try {
            await userdoc.SetAsync(useraccount);
        }
        catch (Exception e) {
            Console.WriteLine($"Error saving data: {e.Message}");
        }


    }
    public async Task CreateUserAndExercise(Users user, Quests quest)
    {
        if (user == null) throw new ArgumentNullException(nameof(quest));
        if (quest == null) throw new ArgumentNullException(nameof(quest));

        await SetUpFireStore(); // Ensure this is optimized and not redundantly called.

        string username = user.UserName;
        string exerciseTitle = quest.Title;

        // Document reference
        DocumentReference userDoc = db.Collection("User").Document(username);
        DocumentReference exerciseDoc = userDoc.Collection("Quest").Document(exerciseTitle);
        try
        {
            // Save the exercise object
            await userDoc.SetAsync(user);
            await exerciseDoc.SetAsync(quest);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
            // Additional error handling (e.g., retry or logging)
        }
    }
    /*public async Task<List<Quests>> GetQuestsAsync(string username)
    {
        try
        {
            // Initialize Firestore
            await SetUpFireStore();

            // Prepare a list to store quests
            var Questtitle = new List<Quests>();

            // Access the Quests subcollection under the User document
            var collection = await db.Collection("User")
                                     .Document(username)
                                     .Collection("Quests")
                                     .GetSnapshotAsync();

            foreach (var document in collection.Documents) {
                if (document.Exists) {
                    Quests quests = document.ConvertTo<Quests>();
                    Questtitle.Add(quests);
                }
            }
            return Questtitle;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching quests for username {username}: {ex.Message}");
            return new List<Quests>(); // Return an empty list in case of an error
        }
    }

    */
    /* public async Task StartListeningToCollection(string collectionName, Action<List<Dictionary<string, object>>> onDataUpdate)
     {
         await SetUpFireStore();
         var collectionReference = db.Collection(collectionName);

         // Listen for real-time updates
         collectionReference.Listen(snapshot =>
         {
             var dataList = new List<Dictionary<string, object>>();

             foreach (var documentSnapshot in snapshot.Documents)
             {
                 var data = documentSnapshot.ToDictionary();
                 dataList.Add(data);
             }

             // Call the provided action with updated data
             onDataUpdate(dataList);
         });
     }*/
    public async Task<List<Quests>> GetQuestsAsync(string username)
    {
        await SetUpFireStore();
        var data = await db
                        .Collection("User").Document(username).Collection("Quest")
                        .GetSnapshotAsync();
        var quests = data.Documents
            .Select(doc =>
            {
                var exercises = doc.ConvertTo<Quests>();
                exercises.Id = doc.Id; // FirebaseId hinzufügen
                return exercises;
            })
            .ToList();
        return quests;
    }
    public async Task<string> GetUserNameAsync(string email)
    {
        try
        {
            // Setup Firestore connection
            await SetUpFireStore();

            // Reference the document in the "UserAccount" collection
            DocumentReference docRef = db.Collection("UserAccount").Document(email);

            // Get the document snapshot
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Retrieve the "UserName" field from the document
                string userName = snapshot.GetValue<string>("UserName");
                return userName;
            }
            else
            {
                Console.WriteLine($"Document with email {email} does not exist.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving UserName: {ex.Message}");
            return null;
        }
    }

    public async Task DeleteDocumentAsync(string collectionName, string documentId)
    {
        DocumentReference docRef = db.Collection(collectionName).Document(documentId);
        await docRef.DeleteAsync();
       
    }
}


