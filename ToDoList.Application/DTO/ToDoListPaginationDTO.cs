namespace ToDoList.Application.DTO;

public record ToDoListPaginationDTO(string UserId, 
                                    int CurrentPage = 1, 
                                    int PageSize = 10)
{
}
