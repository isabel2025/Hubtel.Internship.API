using System;
using System.Net;
using Hubtel.Internship.Api.Interfaces;
using Hubtel.Internship.Api.Models;
using Hubtel.ProducerMessaging.Api.Models;
using Newtonsoft.Json;

namespace Hubtel.Internship.Api.Services
{
	public class TaskService : ITaskService
	{
        private readonly ILogger<TaskService> _logger;
        private readonly ApplicationDbContext _dbContext;


        public TaskService(ILogger<TaskService> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<ApiResponse<List<TaskModel>>> GetTasks(string userId)
        {
            throw new NotImplementedException();
        }
         
        public async Task<ApiResponse<TaskModel>> AddTask(TaskModel model)
        { 
            try
            {
                _logger.LogDebug("About to add a task. Payload: {payload}", JsonConvert.SerializeObject(model));

                var modelToDb = new Tasks
                {
                    Title = model.Title,
                    Description = model.Description,
                    UserId = model.UserId,
                    Status = "active",

                };
               var saveResponse = await _dbContext.Tasks.AddAsync(modelToDb);

               await _dbContext.SaveChangesAsync();

                if (saveResponse.IsKeySet)
                {
                    _logger.LogDebug("Task has been created successfully. UserId: {userid}", model.UserId);
                    
                    return new ApiResponse<TaskModel>
                    {
                        Status = "true",
                        Code = $"{(int)HttpStatusCode.OK}",
                        Message = "Successfully added Task",
                        Data = model
                    };
                }
                return new ApiResponse<TaskModel>
                {
                    Status = "false",
                    Code = $"{(int)HttpStatusCode.BadRequest}",
                    Message = "failed to added Task",
                    Data = new TaskModel()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured creating a task. Error: {error}", ex.ToString());
                
                return new ApiResponse<TaskModel>
                {
                    Status = "false",
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = "Oops something bad happened",
                    Data = new TaskModel()
                };
            }

        }
    }
}

