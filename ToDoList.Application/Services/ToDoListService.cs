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
        var toDoList = await _repository.GetByIdAsync(deleteDTO.ToDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("ToDoList entity with provided Id does not exist", deleteDTO.ToDoListId);
        else if (!toDoList.CanBeDeletedBy(deleteDTO.UserId))
            throw new BadRequestException("You cannot share this list with anyone as it`s not yours.", deleteDTO);

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
            throw new NotFoundException("ToDoList entity with provided Id does not exist", id);

        return toDoList.ToToDoListDTO();
    }

    public async Task<ISet<string>> GetRelationsAsync(ToDoListRelationsDTO relationsDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(relationsDTO.ToDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("ToDoList entity with provided Id does not exist", relationsDTO.ToDoListId);
        else if (!toDoList.CanBeAccessedBy(relationsDTO.UserId))
            throw new BadRequestException("You cannot share this list with anyone as it`s not yours.", relationsDTO);

        return toDoList.SharedTo;
    }

    public async Task ShareToUserAsync(ShareToDoListDTO shareDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(shareDTO.ToDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("ToDoList entity with provided Id does not exist", shareDTO.ToDoListId);
        else if (!toDoList.CanBeAccessedBy(shareDTO.UserId))
            throw new BadRequestException("You cannot share this list with anyone as it`s not yours.", shareDTO);
        else if (!toDoList.SharedTo.Add(shareDTO.SharedUserId))
            throw new BadRequestException($"The user-{shareDTO.SharedUserId} already has access to the to-do list.", shareDTO);

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    public async Task UnshareFromUserAsync(UnshareToDoListDTO unshareDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(unshareDTO.ToDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("ToDoList entity with provided Id does not exist", unshareDTO.ToDoListId);
        else if (!toDoList.CanBeAccessedBy(unshareDTO.UserId))
            throw new BadRequestException("You cannot manipulate this list with anyone as it`s not yours.", unshareDTO);
        else if (!toDoList.SharedTo.Remove(unshareDTO.SharedUserId))
            throw new BadRequestException($"The user-{unshareDTO.SharedUserId} already doesn`t have an access to the to-do list.", unshareDTO);

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    public async Task UpdateAsync(UpdateToDoListDTO updateDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(updateDTO.Id, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException("ToDoList entity with provided Id does not exist", updateDTO.Id);
        else if (!toDoList.CanBeAccessedBy(updateDTO.UserId))
            throw new BadRequestException("Something wrong here!", updateDTO);

        toDoList.Name = updateDTO.Name;

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }
}
