namespace ToDoList.Application.DTO;

public record UpdateToDoListDTO(string Id,
                                string Name,
                                string UserId)
{
}
