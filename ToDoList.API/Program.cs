
using ToDoList.API.Common;
using ToDoList.Application.Extensions;
using ToDoList.Infrastructure.Extensions;

namespace ToDoList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterInfrastructureOptions(builder.Configuration);
            builder.Services.RegisterMongoToDoContext();
            builder.Services.RegisterRepositories();
            builder.Services.RegisterServices();

            builder.Services.RegisterFluentValidation();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
