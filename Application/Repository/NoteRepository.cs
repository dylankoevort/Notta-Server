using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Application.Helpers;
using Infrastructure;
using Models;

public class NoteRepository : INoteRepository
{
    private readonly FirebaseContext _firebaseContext;
    private const string UsersCollectionName = "users";
    private const string NotesCollectionName = "notes";

    public NoteRepository(FirebaseContext firebaseContext)
    {
        _firebaseContext = firebaseContext ?? throw new ArgumentNullException(nameof(firebaseContext));
    }

    public async Task<IEnumerable<Note>> GetAllNotes()
    {
        var usersCollection = _firebaseContext.Database.Collection(UsersCollectionName);
        var allNotes = new List<Note>();
        var usersQuerySnapshot = await usersCollection.GetSnapshotAsync();
        foreach (var userDocument in usersQuerySnapshot.Documents)
        {
                var notesCollection = userDocument.Reference.Collection(NotesCollectionName);
                var notesQuerySnapshot = await notesCollection.GetSnapshotAsync();
                foreach (var noteDocument in notesQuerySnapshot.Documents)
                {
                    allNotes.Add(noteDocument.ConvertTo<Note>());
                }
        }

        return allNotes;
    }

    public async Task<Note> GetNoteById(string userId, string noteId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId);
        var noteDocument = userRef.Collection(NotesCollectionName).Document(noteId);
        var documentSnapshot = await noteDocument.GetSnapshotAsync();
        return documentSnapshot.Exists ? documentSnapshot.ConvertTo<Note>() : null;
    }
    
    public async Task<IEnumerable<Note>> GetNotesByUserId(string userId)
    {
        try
        {
            var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId);
            var notesCollection = userRef.Collection(NotesCollectionName);
            var querySnapshot = await notesCollection.GetSnapshotAsync();
            var notes = new List<Note>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                notes.Add(documentSnapshot.ConvertTo<Note>());
            }

            return notes;
        } 
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Note>> GetNotesByNotebookId(string userId, string notebookId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId);
        var notesCollection = userRef.Collection(NotesCollectionName);
        var querySnapshot = await notesCollection.WhereEqualTo("NotebookId", notebookId).GetSnapshotAsync();
        var notes = new List<Note>();
        foreach (var documentSnapshot in querySnapshot.Documents)
        {
            notes.Add(documentSnapshot.ConvertTo<Note>());
        }

        return notes;
    }

    public async Task<Note> CreateNote(string userId, Note note)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId);
        var noteCollection = userRef.Collection(NotesCollectionName);
        note.NoteId = Helpers.GenerateNewNoteId(note).ToUpper();
        note.UserId = userId;
        note.NotebookId = "nb_" + userId;
        note.DateCreated = DateTime.UtcNow;
        note.DateUpdated = DateTime.UtcNow;
        await noteCollection.Document(note.NoteId).SetAsync(note);
        return note;
    }

    public async Task<Note> UpdateNote(string userId, Note note)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId);
        var noteDocument = userRef.Collection(NotesCollectionName).Document(note.NoteId);
        note.NotebookId = "nb_" + userId;
        note.NotebookId = "nb_" + userId;
        note.DateUpdated = DateTime.UtcNow;
        await noteDocument.SetAsync(note, SetOptions.MergeAll);
        return note;
    }
    
    public async Task DeleteNote(string userId, string noteId)
    {
        var userRef = _firebaseContext.Database.Collection(UsersCollectionName).Document(userId);
        var noteDocument = userRef.Collection(NotesCollectionName).Document(noteId);
        await noteDocument.DeleteAsync();
    }
}
