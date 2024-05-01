using Google.Cloud.Firestore;
using Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Azure.Storage.Blobs;

public class FirebaseContext
{
    private FirestoreDb _db;
    private readonly ILogger<FirebaseContext> _logger;

    public FirebaseContext(ILogger<FirebaseContext> logger)
    {
        _logger = logger;
        
        string projectId = "notta-418218";
        string storageConnectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString");
        
        _logger.LogInformation("Creating Firestore client...");
        _logger.LogInformation($"Azure Blob Connection String: {storageConnectionString}");
        try
        {
            // Download the service account JSON file from Azure Blob Storage
            var blobServiceClient = new BlobServiceClient(storageConnectionString);
            string containerName = "firebase-container";
            string blobName = "notta-418218-324b2039a6ce.json";
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var blobDownloadInfo = blobClient.Download();

            // Read the JSON file contents
            using (var stream = new MemoryStream())
            {
                blobDownloadInfo.Value.Content.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                
                // Set the GOOGLE_APPLICATION_CREDENTIALS environment variable to the JSON file contents
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "firebase.json");

                // Write the JSON file contents to a local file
                using (var fileStream = File.Create("firebase.json"))
                {
                    stream.CopyTo(fileStream);
                }
            }
            
            // Create Firestore client with custom converters
            FirestoreDbBuilder firestoreBuilder = new FirestoreDbBuilder
            {
                ProjectId = projectId,
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
            Console.WriteLine("Error creating Firestore client: " + ex.Message);
            // Print the stack trace for more detailed error information
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
            
            _logger.LogError(ex, "Error creating Firestore client");
            throw;
        }
    }

    public FirestoreDb Database
    {
        get { return _db; }
    }
}