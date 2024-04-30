using Google.Cloud.Firestore;
using Models;
using System;

public class FirebaseContext
{
    private FirestoreDb _db;

    public FirebaseContext()
    {
        string projectId = "notta-418218";
        string keyFile = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

        try
        {
            if (string.IsNullOrEmpty(keyFile))
            {
                throw new Exception("Google Application Credentials environment variable is not set.");
            }

            // Set the environment variable for GOOGLE_APPLICATION_CREDENTIALS
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyFile);
            Console.WriteLine("GOOGLE_APPLICATION_CREDENTIALS: " + keyFile);

            // Create Firestore client with custom converters
            FirestoreDbBuilder firestoreBuilder = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                CredentialsPath = keyFile
            };
            
            firestoreBuilder.ConverterRegistry = new ConverterRegistry();

            // Register custom converters
            firestoreBuilder.ConverterRegistry.Add(new UserConverter());
            firestoreBuilder.ConverterRegistry.Add(new NotebookConverter());
            firestoreBuilder.ConverterRegistry.Add(new NoteConverter());

            // Create Firestore client
            _db = firestoreBuilder.Build();
        }
        catch (Exception ex)
        {
            // Log or handle exceptions during Firestore client creation
            Console.WriteLine("Error creating Firestore client:", ex);
        }
    }

    public FirestoreDb Database
    {
        get { return _db; }
    }
}