

using Google.Cloud.Firestore;

namespace PeakForm.Model
{
    public class Penalty {
        [FirestoreProperty]
        public string PenaltyItems {
            get;
            set;
        }
    }
}
