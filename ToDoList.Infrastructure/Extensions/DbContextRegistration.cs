using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Contexts;

namespace ToDoList.Infrastructure.Extensions;

public static class DbContextRegistration
{
    public static IServiceCollection RegisterMongoToDoContext(this IServiceCollection services)
    {
        services.AddSingleton<ToDoMongoDbContext>();

        return services;
    }
}
