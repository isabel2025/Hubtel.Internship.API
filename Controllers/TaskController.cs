using Hubtel.Internship.Api.Interfaces;
using Hubtel.Internship.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hubtel.Internship.Api.Controllers;

[ApiController]
[Route("api/todo-app")]
public class TaskController : ControllerBase
{


    private readonly ILogger<TaskController> _logger;
    private readonly ITaskService _taskService;

    public TaskController(ILogger<TaskController> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return Ok();

    }

    [HttpPost("add-task")]
    public async Task<IActionResult> AddTask([FromBody] TaskModel model)
    {
        await _taskService.AddTask(model);
        await Task.CompletedTask;
        return Ok();
    }
}

