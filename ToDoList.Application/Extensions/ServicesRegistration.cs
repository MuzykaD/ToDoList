using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Services;

namespace ToDoList.Application.Extensions;

public static class ServicesRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IToDoListService, ToDoListService>();

        return services;
    }
}
