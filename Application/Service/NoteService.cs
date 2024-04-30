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

    public async Task<IEnumerable<Note>> GetAllNotes()
    {
        return await _noteRepository.GetAllNotes();
    }

    public async Task<Note> GetNoteById(string userId,string noteId)
    {
        return await _noteRepository.GetNoteById(userId, noteId);
    }
    
    public async Task<IEnumerable<Note>> GetNotesByUserId(string userId)
    {
        return await _noteRepository.GetNotesByUserId(userId);
    }
    
    public async Task<IEnumerable<Note>> GetNotesByNotebookId(string userId, string notebookId)
    {
        return await _noteRepository.GetNotesByNotebookId(userId, notebookId);
    }
    
    public async Task<Note> CreateNote(string userId, Note note)
    {
        return await _noteRepository.CreateNote(userId, note);
    }

    public async Task<Note> UpdateNote(string userId, Note note)
    {
        return await _noteRepository.UpdateNote(userId, note);
    }

    public async Task DeleteNote(string userId, string noteId)
    {
        await _noteRepository.DeleteNote(userId, noteId);
    }
}