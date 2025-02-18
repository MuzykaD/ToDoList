using ToDoList.Application.DTO;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Mappings;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Services;

public class ToDoListService(IToDoListRepository repository) : IToDoListService
{
    private readonly IToDoListRepository _repository = repository;

    public async Task<ToDoListDTO> CreateAsync(CreateToDoListDTO dto, CancellationToken cancellationToken = default)
    {
        var toDoList = new Domain.Entities.ToDoList(dto.Name, dto.UserId);

        await _repository.CreateAsync(toDoList, cancellationToken);

        return toDoList.ToToDoListDTO();
    }

    public async Task DeleteAsync(DeleteToDoListDTO deleteDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await GetAndValidateAccess(deleteDTO.ToDoListId, deleteDTO.UserId, deleteDTO, cancellationToken);

        if (!toDoList.CanBeDeletedBy(deleteDTO.UserId))
            throw new BadRequestException("You are not allowed to delete this to-do list.", deleteDTO);

        await _repository.DeleteByIdAsync(deleteDTO.ToDoListId, cancellationToken);
    }

    public async Task<ICollection<ToDoListShortDTO>> GetAsync(ToDoListPaginationDTO paginationDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetAsync(paginationDTO.UserId, 
                                                  paginationDTO.CurrentPage, 
                                                  paginationDTO.PageSize, 
                                                  cancellationToken);

        return toDoList.Select(td => td.ToToDoListShortDTO()).ToList();
    }

    public async Task<ToDoListDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(id, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("To-do list with provided Id does not exist.", id);

        return toDoList.ToToDoListDTO();
    }

    public async Task<ISet<string>> GetRelationsAsync(ToDoListRelationsDTO relationsDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await GetAndValidateAccess(relationsDTO.ToDoListId, relationsDTO.UserId, relationsDTO, cancellationToken);

        return toDoList.SharedTo;
    }

    public async Task ShareToUserAsync(ShareToDoListDTO shareDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await GetAndValidateAccess(shareDTO.ToDoListId, shareDTO.UserId, shareDTO, cancellationToken);

        if (!toDoList.SharedTo.Add(shareDTO.SharedUserId))
            throw new BadRequestException($"User-{shareDTO.SharedUserId} already has access to the to-do list.", shareDTO);

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    public async Task UnshareFromUserAsync(UnshareToDoListDTO unshareDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await GetAndValidateAccess(unshareDTO.ToDoListId, unshareDTO.UserId, unshareDTO, cancellationToken);

        if (!toDoList.SharedTo.Remove(unshareDTO.SharedUserId))
            throw new BadRequestException($"User-{unshareDTO.SharedUserId} already does not have access to the to-do list.", unshareDTO);

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    public async Task UpdateAsync(UpdateToDoListDTO updateDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await GetAndValidateAccess(updateDTO.Id, updateDTO.UserId, updateDTO, cancellationToken);

        toDoList.Name = updateDTO.Name;

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    private async Task<Domain.Entities.ToDoList> GetAndValidateAccess<TRequest>(string toDoListId, string userId, TRequest? request, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(toDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("To-do list with provided Id does not exist.", toDoListId);
        else if (!toDoList.CanBeAccessedBy(userId))
            throw new BadRequestException($"User-{userId} is not allowed to access this to-do list.", request);

        return toDoList;
    }
}
