using Application;
using Infrastructure;
using Models;

namespace Repository;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Note> GetNotesByUserId(int userId)
    {
        return _context.Notes.Where(n => n.UserId == userId).ToList() ?? throw new InvalidOperationException();
    }
    
    public IEnumerable<Note> GetNotesByUserId(string userUid)
    {
        return _context.Notes.Where(n => n.UserId.Equals(userUid)).ToList() ?? throw new InvalidOperationException();
    }

    public Note GetNoteById(int noteId)
    {
        return _context.Notes.FirstOrDefault(n => n.NoteId == noteId) ?? throw new InvalidOperationException();
    }

    public void AddNote(Note note)
    {
        _context.Notes.Add(note);
        _context.SaveChanges();
    }

    public void UpdateNote(Note note)
    {
        _context.Notes.Update(note);
        _context.SaveChanges();
    }

    public void DeleteNote(int noteId)
    {
        var note = _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
        _context.Notes.Remove(note);
        _context.SaveChanges();
    }
}