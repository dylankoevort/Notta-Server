using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using Models;

public class NotebookRepository : INotebookRepository
{
    private readonly FirebaseContext _firebaseContext;
    private readonly INoteRepository _noteRepository;
    private const string UsersCollectionName = "users";

    public NotebookRepository(FirebaseContext firebaseContext, INoteRepository noteRepository)
    {
        _firebaseContext = firebaseContext ?? throw new ArgumentNullException(nameof(firebaseContext));
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    public async Task<IEnumerable<Notebook>> GetAllNotebooks()
    {
        var notebooksCollection = _firebaseContext.Database.Collection(UsersCollectionName);

        var notebooks = new List<Notebook>();

        // Get all documents from the users collection
        var querySnapshot = await notebooksCollection.GetSnapshotAsync();

        // Iterate over each user document
        foreach (var userDocument in querySnapshot.Documents)
        {
            // Get the notebooks subcollection for each user
            var notebookCollection = userDocument.Reference.Collection("notebooks");

            // Get all documents from the notebooks subcollection
            var notebookQuerySnapshot = await notebookCollection.GetSnapshotAsync();

            // Iterate over each notebook document and add it to the list
            foreach (var notebookDocument in notebookQuerySnapshot.Documents)
            {
                notebooks.Add(notebookDocument.ConvertTo<Notebook>());
            }
        }

        return notebooks;
    }

    public async Task<Notebook> GetNotebookById(string userId, string notebookId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookDocument = userRef.Collection("notebooks").Document(notebookId.ToString());
        var documentSnapshot = await notebookDocument.GetSnapshotAsync();
        var notebook = documentSnapshot.Exists ? documentSnapshot.ConvertTo<Notebook>() : null;
        if (notebook != null)
        {
            // Fetch notes for this notebook
            notebook.Notes = await _noteRepository.GetNotesByNotebookId(userId, notebookId);
        }
        return notebook;
    }
    
    public async Task<IEnumerable<Notebook>> GetNotebooksByUserId(string userId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookCollection = userRef.Collection("notebooks");

        var querySnapshot = await notebookCollection.GetSnapshotAsync();

        var notebooks = new List<Notebook>();
        foreach (var documentSnapshot in querySnapshot.Documents)
        {
            notebooks.Add(documentSnapshot.ConvertTo<Notebook>());
        }
        return notebooks;
    }
    
    public async Task<Notebook> CreateNotebook(string userId, Notebook notebook)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookCollection = userRef.Collection("notebooks");
        var documentReference = await notebookCollection.AddAsync(notebook);
        notebook.NotebookId = documentReference.Id;
        return notebook;
    }

    public async Task<Notebook> UpdateNotebook(string userId, Notebook notebook)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookDocument = userRef.Collection("notebooks").Document(notebook.NotebookId.ToString());
        await notebookDocument.SetAsync(notebook, SetOptions.MergeAll);
        return notebook;
    }
    
    public async Task DeleteNotebook(string userId, string notebookId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookDocument = userRef.Collection("notebooks").Document(notebookId.ToString());
        await notebookDocument.DeleteAsync();
    }
}
