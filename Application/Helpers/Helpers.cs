using Google.Cloud.Firestore;

namespace Application.Helpers;

public class Helpers
{
    public static DateTime? ConvertFirestoreTimestamp(object timestamp)
    {
        if (timestamp is Timestamp firestoreTimestamp)
        {
            return firestoreTimestamp.ToDateTime();
        }
        return null;
    }
}