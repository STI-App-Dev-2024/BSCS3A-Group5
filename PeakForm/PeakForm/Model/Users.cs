

using Google.Cloud.Firestore;


namespace PeakForm.Model;
[FirestoreData]
public class Users {
    


    [FirestoreProperty]
    public string ID { get; set; }  
    [FirestoreProperty]
    public string UserName { get; set; }

    [FirestoreProperty]
    public double BMI { get; set; }
    [FirestoreProperty]
    public string BodyType { get; set; }
    [FirestoreProperty]
    public DateTime CreateAt { get; set; }


}
