using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController(IToDoListService service) : ControllerBase
{
    private readonly IToDoListService _service = service;

    [HttpGet("{toDoListId}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string toDoListId)
    {
        var toDoList = await _service.GetByIdAsync(toDoListId);

        return Ok(toDoList);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] ToDoListPaginationDTO paginationDTO)
    {
        var toDoLists = await _service.GetAsync(paginationDTO);

        return Ok(toDoLists);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateToDoListDTO createDTO)
    {
        var createdToDoList = await _service.CreateAsync(createDTO);

        return Ok(createdToDoList);     
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateToDoListDTO updateDTO)
    {
        await _service.UpdateAsync(updateDTO);

        return NoContent();
    }

    [HttpPatch("/share")]
    public async Task<IActionResult> ShareToUserAsync([FromBody] ShareToDoListDTO shareDTO)
    {
        await _service.ShareToUserAsync(shareDTO);

        return NoContent();
    }

    [HttpPatch("/unshare")]
    public async Task<IActionResult> UnshareFromUserAsync([FromBody] UnshareToDoListDTO unshareDTO)
    {
        await _service.UnshareFromUserAsync(unshareDTO);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteToDoListDTO deleteDTO)
    {
        await _service.DeleteAsync(deleteDTO);

        return NoContent();
    }
}
