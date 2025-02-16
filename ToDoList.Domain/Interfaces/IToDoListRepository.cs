namespace ToDoList.Domain.Interfaces;

public interface IToDoListRepository
{
    Task<Entities.ToDoList?> GetByIdAsync(Entities.ToDoList toDoList, CancellationToken cancellationToken = default);

    Task<ICollection<Entities.ToDoList>> GetAsync(CancellationToken cancellationToken = default);

    Task CreateAsync(Entities.ToDoList todoList, CancellationToken cancellationToken = default);

    Task UpdateAsync(Entities.ToDoList updatedList, CancellationToken cancellationToken = default);

    Task DeleteAsync(CancellationToken cancellationToken = default);
}
