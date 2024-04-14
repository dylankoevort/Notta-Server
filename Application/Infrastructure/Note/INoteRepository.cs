using Models;
namespace Infrastructure;

public interface INoteRepository
{
    IEnumerable<Note> GetAllNotes();
    IEnumerable<Note> GetNotesByUserId(int userId);
    Note GetNoteById(int noteId);
    Note GetNoteBySlug(string noteSlug);
    void AddNote(Note note);
    void UpdateNote();
    void DeleteNote(int noteId);
}