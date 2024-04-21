using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using Models;

public class NoteRepository : INoteRepository
{
    private readonly FirebaseContext _firebaseContext;
    private const string UsersCollectionName = "users";

    public NoteRepository(FirebaseContext firebaseContext)
    {
        _firebaseContext = firebaseContext ?? throw new ArgumentNullException(nameof(firebaseContext));
    }

    public async Task<IEnumerable<Note>> GetAllNotes()
    {
        var usersCollection = _firebaseContext.Database.Collection(UsersCollectionName);

        var allNotes = new List<Note>();

        // Get all documents from the users collection
        var usersQuerySnapshot = await usersCollection.GetSnapshotAsync();

        // Iterate over each user document
        foreach (var userDocument in usersQuerySnapshot.Documents)
        {
            // Get the notebooks subcollection for each user
            var notebooksCollection = userDocument.Reference.Collection("notebooks");

            // Get all documents from the notebooks subcollection
            var notebooksQuerySnapshot = await notebooksCollection.GetSnapshotAsync();

            // Iterate over each notebook document
            foreach (var notebookDocument in notebooksQuerySnapshot.Documents)
            {
                // Get the notes subcollection for each notebook
                var notesCollection = notebookDocument.Reference.Collection("notes");

                // Get all documents from the notes subcollection
                var notesQuerySnapshot = await notesCollection.GetSnapshotAsync();

                // Iterate over each note document and add it to the list
                foreach (var noteDocument in notesQuerySnapshot.Documents)
                {
                    allNotes.Add(noteDocument.ConvertTo<Note>());
                }
            }
        }

        return allNotes;
    }

    public async Task<Note> GetNoteById(string userId, string notebookId, string noteId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookRef = userRef.Collection("notebooks").Document(notebookId.ToString());
        var noteDocument = notebookRef.Collection("notes").Document(noteId.ToString());
        var documentSnapshot = await noteDocument.GetSnapshotAsync();
        return documentSnapshot.Exists ? documentSnapshot.ConvertTo<Note>() : null;
    }
    
    public async Task<IEnumerable<Note>> GetNotesByUserId(string userId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebooksQuerySnapshot = await userRef.Collection("notebooks").GetSnapshotAsync();

        var notes = new List<Note>();
        foreach (var notebookDocument in notebooksQuerySnapshot.Documents)
        {
            var notebookId = notebookDocument.Id;
            var notesCollection = notebookDocument.Reference.Collection("notes");
            var notesQuerySnapshot = await notesCollection.GetSnapshotAsync();

            foreach (var noteDocument in notesQuerySnapshot.Documents)
            {
                notes.Add(noteDocument.ConvertTo<Note>());
            }
        }
        return notes;
    }

    public async Task<IEnumerable<Note>> GetNotesByNotebookId(string userId, string notebookId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookRef = userRef.Collection("notebooks").Document(notebookId.ToString());
        var notesCollection = notebookRef.Collection("notes");

        var querySnapshot = await notesCollection.GetSnapshotAsync();

        var notes = new List<Note>();
        foreach (var documentSnapshot in querySnapshot.Documents)
        {
            notes.Add(documentSnapshot.ConvertTo<Note>());
        }

        return notes;
    }

    public async Task<Note> CreateNote(string userId, string notebookId, Note note)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookRef = userRef.Collection("notebooks").Document(notebookId.ToString());
        var noteCollection = notebookRef.Collection("notes");
        var documentReference = await noteCollection.AddAsync(note);
        note.NoteId = documentReference.Id;
        return note;
    }

    public async Task<Note> UpdateNote(string userId, string notebookId, Note note)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookRef = userRef.Collection("notebooks").Document(notebookId.ToString());
        var noteDocument = notebookRef.Collection("notes").Document(note.NoteId.ToString());
        await noteDocument.SetAsync(note, SetOptions.MergeAll);
        return note;
    }
    
    public async Task DeleteNote(string userId, string notebookId, string noteId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId.ToString());
        var notebookRef = userRef.Collection("notebooks").Document(notebookId.ToString());
        var noteDocument = notebookRef.Collection("notes").Document(noteId.ToString());
        await noteDocument.DeleteAsync();
    }
}
