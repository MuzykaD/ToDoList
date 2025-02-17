namespace ToDoList.Application.DTO;

public record UnshareToDoListDTO(string UserId, string SharedUserId, string ToDoListId)
{
}
