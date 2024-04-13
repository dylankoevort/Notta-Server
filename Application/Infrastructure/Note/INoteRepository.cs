using Models;
namespace Infrastructure;

public interface INoteRepository
{
    IEnumerable<Note> GetNotesByUserId(int userId);
    IEnumerable<Note> GetNotesByUserId(string userUid);
    Note GetNoteById(int noteId);
    void AddNote(Note note);
    void UpdateNote(Note note);
    void DeleteNote(int noteId);
}