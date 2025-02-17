using ToDoList.Application.DTO;

namespace ToDoList.Application.Interfaces;

public interface IToDoListService
{
    Task<ToDoListDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<ICollection<ToDoListShortDTO>> GetAsync(ToDoListPaginationDTO paginationDTO, CancellationToken cancellationToken = default);

    Task CreateAsync(CreateToDoListDTO createDTO, CancellationToken cancellationToken = default);

    Task UpdateAsync(UpdateToDoListDTO updateDTO, CancellationToken cancellationToken = default);

    Task DeleteAsync(DeleteToDoListDTO deleteDTO, CancellationToken cancellationToken = default);

    Task ShareToUserAsync(ShareToDoListDTO shareDTO, CancellationToken cancellationToken = default);

    Task UnshareFromUserAsync(UnshareToDoListDTO unshareDTO, CancellationToken cancellationToken = default);

}
