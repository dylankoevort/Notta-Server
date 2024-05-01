using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;

namespace Application;

public class DatabaseTester
{
    private readonly ILogger<FirebaseContext> _logger;
    
    public DatabaseTester(ILogger<FirebaseContext> logger)
    {
        _logger = logger;
    }

    public bool TestConnection()
    {
        try
        {   
            FirebaseContext firebaseContext = new FirebaseContext(_logger);
            var db = firebaseContext.Database;

            var users = db.Collection("users").GetSnapshotAsync().Result;
            
            Console.WriteLine("Firestore initialized successfully!");
            _logger.LogInformation("Firestore initialized successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed to Firestore: {ex.Message}");
            _logger.LogError(ex, "Connection failed to Firestore");
            return false;
        }
    }
}
