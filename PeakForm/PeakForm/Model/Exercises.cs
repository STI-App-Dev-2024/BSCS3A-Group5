using Google.Cloud.Firestore;

namespace PeakForm.Model;
[FirestoreData]
public class Exercises {
    [FirestoreProperty]
    public string ID { 
        set; 
        get;
    }
    [FirestoreProperty]
    public string Title {
        set;
        get;
    }
    
    [FirestoreProperty]
    public string FirstDescription {
        set;
        get;

    }
    [FirestoreProperty]
    public int FirstExerciseSet 
    {
        set;
        get;
    }
    [FirestoreProperty]
    public string SecondDescription {
        set;
        get;
    }
    [FirestoreProperty]
    public int SecondExerciseSet {
        set;
        get;
    }
    [FirestoreProperty]
    public string ThirdDescription
    {
        set;
        get;
        
    }
    [FirestoreProperty]
    public int ThirdexerciseSet 
    {
        set;
        get;
    }
}
