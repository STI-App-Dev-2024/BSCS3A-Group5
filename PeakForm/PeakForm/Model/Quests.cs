
using Google.Cloud.Firestore;

namespace PeakForm.Model;
[FirestoreData]
public class Quests
{
    [FirestoreProperty]
    public string Id { 
        get; 
        set; 
    }
    [FirestoreProperty]
    public string Title
    {
        get;
        set;
    }
    [FirestoreProperty]
    public string Description
    {
        get;
        set;
    }
    


    
}
