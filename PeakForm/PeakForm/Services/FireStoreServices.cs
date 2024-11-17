
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
        string username = useraccount.UserName;
        DocumentReference userdoc = db.Collection("UserAccount").Document(username);
        try {
            await userdoc.SetAsync(useraccount);
        }
        catch (Exception e) {
            Console.WriteLine($"Error saving data: {e.Message}");
        }


    }
    public async Task CreateUserAndExercise(Users user, Exercises exercise)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (exercise == null) throw new ArgumentNullException(nameof(exercise));

        await SetUpFireStore(); // Ensure this is optimized and not redundantly called.

        string username = user.UserName;
        string exerciseTitle = exercise.Title;

        // Document reference
        DocumentReference userDoc = db.Collection("User").Document(username);
        DocumentReference exerciseDoc = userDoc.Collection("Quest").Document(exerciseTitle);
    

        try
        {
            // Save the exercise object
            await userDoc.SetAsync(user);
            await exerciseDoc.SetAsync(exercise);
            Console.WriteLine("Exercise data saved successfully.");
            Console.WriteLine("added Document ID {0}" , userDoc.Id, exerciseDoc.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
            // Additional error handling (e.g., retry or logging)
        }
    }

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
    public async Task<List<Exercises>> GetExercises(string username)
    {
        await SetUpFireStore();
        var data = await db
                        .Collection("User").Document(username).Collection("Quest")
                        .GetSnapshotAsync();
        var exercises = data.Documents
            .Select(doc =>
            {
                var exercises = doc.ConvertTo<Exercises>();
                exercises.ID = doc.Id; // FirebaseId hinzufügen
                return exercises;
            })
            .ToList();
        return exercises;
    }
    public async Task<List<string>> GetStringsAsync(string username)
    {
        try
        {
            await SetUpFireStore();
            CollectionReference docref = db.Collection("User")
                .Document(username)
                .Collection("Quest");
            QuerySnapshot snapshot = await docref.GetSnapshotAsync();
            return snapshot.Documents.Select(doc => doc.Id).ToList();
        }
        catch (Exception ex) {
            Console.WriteLine($"Error retrieving exercise titles: {ex.Message}");
            return new List<string>();
        }
    }
   /* public async Task<string> GetUserNameAync()
    {
            
        await SetUpFireStore();
        DocumentReference data = db.Collection("UserAccount").GetSnapshotAsync();

        try {
            DocumentSnapshot snapshot = await data.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Console.WriteLine("Document data for {0} document:", snapshot.Id);
                UserAccount Useraccount = snapshot.ConvertTo<UserAccount>();
                return Useraccount.UserName;


            }
            else {
                return null;
            
            }

        } catch (Exception e) {
            return "Document {0} does not exist!";

        }
    }*/


    public async Task DeleteDocumentAsync(string collectionName, string documentId)
    {
        DocumentReference docRef = db.Collection(collectionName).Document(documentId);
        await docRef.DeleteAsync();
       
    }
}


