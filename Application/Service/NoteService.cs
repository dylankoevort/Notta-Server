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

    public async Task<Note> GetNoteById(string userId, string notebookId, string noteId)
    {
        return await _noteRepository.GetNoteById(userId, notebookId, noteId);
    }
    
    public async Task<IEnumerable<Note>> GetNotesByUserId(string userId)
    {
        return await _noteRepository.GetNotesByUserId(userId);
    }
    
    public async Task<IEnumerable<Note>> GetNotesByNotebookId(string userId, string notebookId)
    {
        return await _noteRepository.GetNotesByNotebookId(userId, notebookId);
    }
    
    public async Task<Note> CreateNote(string userId, string notebookId, Note note)
    {
        return await _noteRepository.CreateNote(userId, notebookId, note);
    }

    public async Task<Note> UpdateNote(string userId, string notebookId, Note note)
    {
        return await _noteRepository.UpdateNote(userId, notebookId, note);
    }

    public async Task DeleteNote(string userId, string notebookId, string noteId)
    {
        await _noteRepository.DeleteNote(userId, notebookId, noteId);
    }
}