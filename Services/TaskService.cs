using System.Net;
using Hubtel.Internship.Api.Interfaces;
using Hubtel.Internship.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.Internship.Api.Services;

public class TaskService : ITaskService
{
    private readonly ILogger<TaskService> _logger;
    private readonly ApplicationDbContext _dbContext;

    public TaskService(
        ILogger<TaskService> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<List<TaskModel>>> GetTasks(string userId)
    {
        try
        {
            var tasks = await _dbContext.Tasks
                .Where(task => task.UserId == userId)
                .OrderByDescending(task => task.CreatedAt)
                .Select(task => new TaskModel
                {
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                    UserId = task.UserId,
                    CreatedAt = task.CreatedAt
                })
                .ToListAsync();

            return new ApiResponse<List<TaskModel>>
            {
                Status = "true",
                Code = $"{(int)HttpStatusCode.OK}",
                Message = "Tasks retrieved successfully",
                Data = tasks
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tasks for user {UserId}", userId);

            return new ApiResponse<List<TaskModel>>
            {
                Status = "false",
                Code = $"{(int)HttpStatusCode.InternalServerError}",
                Message = "Unable to retrieve tasks",
                Data = new List<TaskModel>()
            };
        }
    }

    public async Task<ApiResponse<TaskModel>> AddTask(TaskModel model)
    {
        try
        {
            var task = new Tasks
            {
                Title = model.Title,
                Description = model.Description,
                UserId = model.UserId,
                Status = string.IsNullOrWhiteSpace(model.Status) ? "active" : model.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Tasks.AddAsync(task);
            var saved = await _dbContext.SaveChangesAsync();

            if (saved > 0)
            {
                model.Status = task.Status;
                model.CreatedAt = task.CreatedAt;

                return new ApiResponse<TaskModel>
                {
                    Status = "true",
                    Code = $"{(int)HttpStatusCode.Created}",
                    Message = "Task added successfully",
                    Data = model
                };
            }

            return new ApiResponse<TaskModel>
            {
                Status = "false",
                Code = $"{(int)HttpStatusCode.BadRequest}",
                Message = "Task could not be added",
                Data = new TaskModel()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task for user {UserId}", model.UserId);

            return new ApiResponse<TaskModel>
            {
                Status = "false",
                Code = $"{(int)HttpStatusCode.InternalServerError}",
                Message = "Unable to create task",
                Data = new TaskModel()
            };
        }
    }
}
