namespace ToDoList.Infrastructure.Options;

public class ToDoMongoDbOptions
{
    public string ConnectionString { get; init; }

    public string Name { get; init; }
}
