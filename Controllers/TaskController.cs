using Hubtel.Internship.Api.Interfaces;
using Hubtel.Internship.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hubtel.Internship.Api.Controllers;

[ApiController]
[Route("api/todo-app")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("tasks/{userId}")]
    public async Task<IActionResult> GetTasks(string userId)
    {
        var response = await _taskService.GetTasks(userId);

        return response.Code == "200"
            ? Ok(response)
            : StatusCode(StatusCodes.Status500InternalServerError, response);
    }

    [HttpPost("add-task")]
    public async Task<IActionResult> AddTask([FromBody] TaskModel model)
    {
        var response = await _taskService.AddTask(model);

        return response.Code switch
        {
            "201" => StatusCode(StatusCodes.Status201Created, response),
            "400" => BadRequest(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }
}
