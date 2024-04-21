using Infrastructure;
using Models;

namespace Service;

public class NotebookService : INotebookService
{
    private readonly INotebookRepository _notebookRepository;

    public NotebookService(INotebookRepository notebookRepository)
    {
        _notebookRepository = notebookRepository;
    }
    
    public async Task<IEnumerable<Notebook>> GetAllNotebooks()
    {
        return await _notebookRepository.GetAllNotebooks();
    }
    
    public async Task<Notebook> GetNotebookById(string userId, string notebookId)
    {
        return await _notebookRepository.GetNotebookById(userId, notebookId);
    }
    
    public async Task<IEnumerable<Notebook>> GetNotebooksByUserId(string userId)
    {
        return await _notebookRepository.GetNotebooksByUserId(userId);
    }
    
    public async Task<Notebook> CreateNotebook(string userId, Notebook notebook)
    {
        return await _notebookRepository.CreateNotebook(userId, notebook);
    }

    public async Task<Notebook> UpdateNotebook(string userId, Notebook notebook)
    {
        return await _notebookRepository.UpdateNotebook(userId, notebook);
    }

    public async Task DeleteNotebook(string userId, string notebookId)
    {
        await _notebookRepository.DeleteNotebook(userId, notebookId);
    }
}