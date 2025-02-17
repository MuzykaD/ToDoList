using MongoDB.Driver;
using ToDoList.Domain.Interfaces;
using ToDoList.Infrastructure.Contexts;

namespace ToDoList.Infrastructure.Repositories;

public class ToDoListRepository(ToDoMongoDbContext dbContext) : IToDoListRepository
{
    private readonly ToDoMongoDbContext _dbContext = dbContext;

    private IMongoCollection<Domain.Entities.ToDoList> Collection => _dbContext.ToDoLists;

    public async Task CreateAsync(Domain.Entities.ToDoList todoList, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(todoList, cancellationToken: cancellationToken);
    }

    public async Task DeleteByIdAsync(string toDoListId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(GetToDoListIdEqualityFilter(toDoListId), cancellationToken);
    }

    public async Task<ICollection<Domain.Entities.ToDoList>> GetAsync(string currentUserId, int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(GetPaginationFilter(currentUserId))
                               .Skip(--currentPage * pageSize)
                               .Limit(pageSize)
                               .SortByDescending(td => td.CreatedAt)
                               .ToListAsync(cancellationToken);

    }

    public async Task<Domain.Entities.ToDoList?> GetByIdAsync(string toDoListId, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(GetToDoListIdEqualityFilter(toDoListId))
                               .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Entities.ToDoList updatedList, CancellationToken cancellationToken = default)
    {
        await Collection.ReplaceOneAsync(GetToDoListIdEqualityFilter(updatedList.Id), 
                                         updatedList, 
                                         cancellationToken: cancellationToken);
    }

    private FilterDefinition<Domain.Entities.ToDoList> GetToDoListIdEqualityFilter(string id)
    {
        return Builders<Domain.Entities.ToDoList>.Filter.Eq(t  => t.Id, id);
    }

    private FilterDefinition<Domain.Entities.ToDoList> GetPaginationFilter(string id)
    {
        return Builders<Domain.Entities.ToDoList>.Filter.Or(
            Builders<Domain.Entities.ToDoList>.Filter.Eq(t => t.UserId, id),
            Builders<Domain.Entities.ToDoList>.Filter.AnyEq(t => t.SharedTo, id));
    }
}
