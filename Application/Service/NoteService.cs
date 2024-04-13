using AutoMapper;
using Infrastructure;
using Models;

namespace Service;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public IEnumerable<Note> GetNotesByUserId(int userId)
    {
        return _noteRepository.GetNotesByUserId(userId);
    }
    
    public IEnumerable<Note> GetNotesByUserId(string userUid)
    {
        return _noteRepository.GetNotesByUserId(userUid);
    }

    public Note GetNoteById(int noteId)
    {
        return _noteRepository.GetNoteById(noteId);
    }

    public void AddNote(Note note)
    {
        _noteRepository.AddNote(note);
    }

    public void UpdateNote(Note note)
    {
        _noteRepository.UpdateNote(note);
    }

    public void DeleteNote(int noteId)
    {
        _noteRepository.DeleteNote(noteId);
    }
}