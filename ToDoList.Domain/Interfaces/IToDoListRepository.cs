namespace ToDoList.Domain.Interfaces;

public interface IToDoListRepository
{
    Task<Entities.ToDoList?> GetByIdAsync(string toDoListId, CancellationToken cancellationToken = default);

    Task<ICollection<Entities.ToDoList>> GetAsync(string currentUserId, int currentPage, int pageSize, CancellationToken cancellationToken = default);

    Task CreateAsync(Entities.ToDoList todoList, CancellationToken cancellationToken = default);

    Task UpdateAsync(Entities.ToDoList updatedList, CancellationToken cancellationToken = default);

    Task DeleteByIdAsync(string toDoListId, CancellationToken cancellationToken = default);
}
