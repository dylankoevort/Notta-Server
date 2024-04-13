using Infrastructure;
using Repository;
using Service;

namespace Application.Service.Config;

public static class ServiceConfiguration
{
    public static IServiceCollection AddService(this IServiceCollection container)
    {
        return container
            .AddScoped<INoteService, NoteService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<INoteRepository, NoteRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}