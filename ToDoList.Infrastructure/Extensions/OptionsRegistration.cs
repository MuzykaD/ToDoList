using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.Extensions;

public static class OptionsRegistration
{
    public static IServiceCollection RegisterInfrastructureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ToDoMongoDbOptions>(options => configuration.GetSection(nameof(ToDoMongoDbOptions))
                                                                       .Bind(options));

        return services;
    }
}
