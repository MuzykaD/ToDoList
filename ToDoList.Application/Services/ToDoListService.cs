using ToDoList.Application.DTO;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Mappings;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Services;

public class ToDoListService(IToDoListRepository repository) : IToDoListService
{
    private readonly IToDoListRepository _repository = repository;

    public async Task CreateAsync(CreateToDoListDTO dto, CancellationToken cancellationToken = default)
    {
        var toDoList = new Domain.Entities.ToDoList(dto.Name, dto.UserId);

        await _repository.CreateAsync(toDoList, cancellationToken);
    }

    public Task DeleteAsync(DeleteToDoListDTO deleteDTO, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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
            throw new NotFoundException<string>("ToDoList entity with provided Id does not exist", id);

        return toDoList.ToToDoListDTO();
    }

    public async Task ShareToUserAsync(ShareToDoListDTO shareDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(shareDTO.ToDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException<string>("ToDoList entity with provided Id does not exist", shareDTO.ToDoListId);
        else if (!toDoList.CanBeModifiedBy(shareDTO.UserId))
            throw new BadRequestException("You cannot share this list with anyone as it`s not yours.");
        else if (!toDoList.SharedTo.Add(shareDTO.SharedUserId))
            throw new BadRequestException($"The user-{shareDTO.SharedUserId} already has access to the to-do list.");

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    public async Task UnshareFromUserAsync(UnshareToDoListDTO unshareDTO, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(unshareDTO.ToDoListId, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException<string>("ToDoList entity with provided Id does not exist", unshareDTO.ToDoListId);
        else if (!toDoList.CanBeModifiedBy(unshareDTO.UserId))
            throw new BadRequestException("You cannot manipulate this list with anyone as it`s not yours.");
        else if (!toDoList.SharedTo.Remove(unshareDTO.SharedUserId))
            throw new BadRequestException($"The user-{unshareDTO.SharedUserId} already doesn`t have an access to the to-do list.");

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }

    public async Task UpdateAsync(UpdateToDoListDTO dto, CancellationToken cancellationToken = default)
    {
        var toDoList = await _repository.GetByIdAsync(dto.Id, cancellationToken);

        if (toDoList == null)
            throw new NotFoundException<string>("ToDoList entity with provided Id does not exist", dto.Id);
        else if (!toDoList.CanBeModifiedBy(dto.UserId))
            throw new BadRequestException("Something wrong here!");

        toDoList.Name = dto.Name;

        await _repository.UpdateAsync(toDoList, cancellationToken);
    }
}
