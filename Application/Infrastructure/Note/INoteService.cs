using Models;

namespace Infrastructure;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotes();
    Task<Note> GetNoteById(string userId, string noteId);
    Task<IEnumerable<Note>> GetNotesByUserId(string userId);
    Task<IEnumerable<Note>> GetNotesByNotebookId(string userId, string notebookId);
    Task<Note> CreateNote(string userId, Note note);
    Task<Note> UpdateNote(string userId, Note note);
    Task DeleteNote(string userId, string noteId);
}