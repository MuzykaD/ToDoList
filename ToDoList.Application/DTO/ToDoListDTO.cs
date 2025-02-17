namespace ToDoList.Application.DTO;

public record ToDoListDTO(string Id,
                          string Name,
                          string UserId,
                          DateTime CreatedAt,
                          ICollection<string> SharedTo)
{
}
