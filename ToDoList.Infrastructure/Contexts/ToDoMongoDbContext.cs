using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.Contexts;

public class ToDoMongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public ToDoMongoDbContext(IOptions<ToDoMongoDbOptions> options)
    {
        _mongoDatabase = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.Name);
    }

    public IMongoCollection<Domain.Entities.ToDoList> ToDoLists 
        => _mongoDatabase.GetCollection<Domain.Entities.ToDoList>(nameof(ToDoLists));
}
