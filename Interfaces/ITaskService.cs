using Hubtel.Internship.Api.Models;

namespace Hubtel.Internship.Api.Interfaces
{
	public interface ITaskService
	{

		public Task<ApiResponse<List<TaskModel>>> GetTasks(string userId);
		public Task<ApiResponse<TaskModel>> AddTask(TaskModel model);

	}
}

