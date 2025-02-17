namespace ToDoList.Application.DTO;

public record ShareToDoListDTO(string UserId, string SharedUserId, string ToDoListId)
{
}
