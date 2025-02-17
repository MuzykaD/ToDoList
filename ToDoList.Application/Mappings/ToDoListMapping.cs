using ToDoList.Application.DTO;

namespace ToDoList.Application.Mappings;

internal static class ToDoListMapping
{
    internal static ToDoListDTO ToToDoListDTO(this Domain.Entities.ToDoList entity)
        => new(entity.Id,
               entity.Name,
               entity.UserId,
               entity.CreatedAt,
               new HashSet<string>(entity.SharedTo));

    internal static ToDoListShortDTO ToToDoListShortDTO(this Domain.Entities.ToDoList entity)
        => new(entity.Id, entity.Name);
}
