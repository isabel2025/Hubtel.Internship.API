using System;
using Hubtel.Internship.Api.Models;

namespace Hubtel.Internship.Api.Interfaces
{
	public interface ITaskService
	{

		public Task<TaskModel> GetTasks(string userId);
		public Task<TaskModel> AddTask(TaskModel model);
	}
}

