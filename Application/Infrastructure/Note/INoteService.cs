using Models;

namespace Infrastructure;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotes();
    Task<Note> GetNoteById(string userId, string notebookId, string noteId);
    Task<IEnumerable<Note>> GetNotesByUserId(string userId);
    Task<IEnumerable<Note>> GetNotesByNotebookId(string userId, string notebookId);
    Task<Note> CreateNote(string userId, string notebookId, Note note);
    Task<Note> UpdateNote(string userId, string notebookId, Note note);
    Task DeleteNote(string userId, string notebookId, string noteId);
}