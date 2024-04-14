using Models;

namespace Infrastructure;

public interface INoteService
{
    IEnumerable<Note> GetAllNotes();
    IEnumerable<Note> GetNotesByUserId(int? userId, string? userUid);
    Note GetNoteById(int noteId);
    Note GetNoteBySlug(string noteSlug);
    void AddNote(Note note);
    void UpdateNote(Note note);
    void DeleteNote(int noteId);
}