using Infrastructure;
using Service;

namespace Application.Service.Config;

public static class ServiceConfiguration
{
    public static IServiceCollection AddService(this IServiceCollection container)
    {
        return container

            .AddScoped<IUserService, UserService>()
            .AddScoped<INotebookService, NotebookService>()
            .AddScoped<INoteService, NoteService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<INotebookRepository, NotebookRepository>()
            .AddScoped<INoteRepository, NoteRepository>();
    }
}