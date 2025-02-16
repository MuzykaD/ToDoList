using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.Contexts;

public class ToDoMongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public ToDoMongoDbContext(IOptions<ToDoMongoDbOptions> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);

        _mongoDatabase = client.GetDatabase(options.Value.Name);
    }

    // TODO: move collection namings to constants somewhere
    public IMongoCollection<Domain.Entities.ToDoList> ToDoLists 
        => _mongoDatabase.GetCollection<Domain.Entities.ToDoList>("ToDoLists");
}
