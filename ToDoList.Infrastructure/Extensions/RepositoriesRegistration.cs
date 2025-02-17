using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Interfaces;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Infrastructure.Extensions;

public static class RepositoriesRegistration
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IToDoListRepository, ToDoListRepository>();

        return services;
    }
}

