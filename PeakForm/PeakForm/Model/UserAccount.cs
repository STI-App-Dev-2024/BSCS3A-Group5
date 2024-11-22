
using Google.Cloud.Firestore;

namespace PeakForm.Model;
[FirestoreData]
public class UserAccount {
    [FirestoreProperty]
    public string Uid {  get; set; }
    [FirestoreProperty]
    public string FirstName { get; set; }
    [FirestoreProperty]
    public string LastName { get; set; }
    [FirestoreProperty]
    public string UserName { get; set; }
    [FirestoreProperty]
    public string Email { get; set; }
 // [FirestoreProperty]
 // public DateOnly Birthdate { get; set; }   
    [FirestoreProperty]
    public float Height { get; set; }
    [FirestoreProperty]
    public float Weight {  get; set; }
    [FirestoreProperty]
    public DateTime CreateAt { get; set; }


 
   
}
public class DateTimeToTimeStampConverter : IFirestoreConverter<DateTime>
{
    public object ToFirestore(DateTime value) => Timestamp.FromDateTime(value.ToUniversalTime());

    public DateTime FromFirestore(object value)
    {
        if (value is Timestamp timestamp)
        {
            return timestamp.ToDateTime();
        }
        throw new ArgumentException("INVALID VALUE!");
    }
}

