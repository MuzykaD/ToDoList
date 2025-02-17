using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Validators;

namespace ToDoList.Application.Extensions;
public static class FluentValidationRegistration 
{
    public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateToDoListValidator>();

        return services;
    }
}
