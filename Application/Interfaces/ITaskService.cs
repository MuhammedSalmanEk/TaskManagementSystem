using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface ITaskService
	{
		Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId);
		Task<IEnumerable<TaskResponseDto>> GetTaskAsync(string userId, bool isAdmin, int? page, int? pageSize);
		Task UpdateTaskAsync(int Id, UpdateTaskDto dto, string userId);
		Task MarkCompletedAsync(int Id);
	}
}

