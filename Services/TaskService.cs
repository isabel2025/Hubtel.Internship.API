using System;
using System.Data;
using System.Net;
using Hubtel.Internship.Api.Interfaces;
using Hubtel.Internship.Api.Models;
using Newtonsoft.Json;
using Npgsql;

namespace Hubtel.Internship.Api.Services
{
	public class TaskService : ITaskService
	{
        private readonly ILogger<TaskService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly string _connectionString;


        public TaskService(ILogger<TaskService> logger, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _connectionString = configuration["ConnectionStrings:DbConnection"];
            // DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // builder.UseMySQL(_connectionString);
        }

        internal IDbConnection Connection => new NpgsqlConnection(_connectionString);

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
                    CreatedAt = DateTime.UtcNow
                    

                };
                
               
                   await _dbContext.Tasks.AddAsync(modelToDb);

               var saveResponse=  await _dbContext.SaveChangesAsync();

                if (saveResponse > 0)
                {
                    _logger.LogDebug("Task has been created successfully. UserId: {userid}", model.UserId);
                    
                    return new ApiResponse<TaskModel>
                    {
                        Status = "true",
                        Code = $"{(int)HttpStatusCode.Created}",
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
            catch(Exception ex)
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

