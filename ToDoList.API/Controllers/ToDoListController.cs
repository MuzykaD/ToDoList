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
    public async Task<IActionResult> GetByIdAsync([FromQuery] string toDoListId)
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
        await _service.CreateAsync(createDTO);

        return NoContent();     
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateToDoListDTO updateDTO)
    {
        await _service.UpdateAsync(updateDTO);

        return NoContent();
    }

   
}
