using Google.Cloud.Firestore;

namespace Application;

public class DatabaseTester
{
    public DatabaseTester()
    {}

    public bool TestConnection()
    {
        try
        {   
            FirebaseContext firebaseContext = new FirebaseContext();
            var db = firebaseContext.Database;

            var users = db.Collection("users").GetSnapshotAsync().Result;
            
            Console.WriteLine("Firestore initialized successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
            return false;
        }
    }
}
