
using Google.Cloud.Firestore;

namespace PeakForm.Model;
public class Quests
{
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
