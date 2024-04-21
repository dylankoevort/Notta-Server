using Models;

namespace Infrastructure;

public interface INotebookService
{
    Task<IEnumerable<Notebook>> GetAllNotebooks();
    Task<Notebook> GetNotebookById(string userId, string notebookId);
    Task<IEnumerable<Notebook>> GetNotebooksByUserId(string userId);
    Task<Notebook> CreateNotebook(string userId, Notebook notebook);
    Task<Notebook> UpdateNotebook(string userId, Notebook notebook);
    Task DeleteNotebook(string userId, string notebookId);
}