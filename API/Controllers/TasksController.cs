using System;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
	[ApiController]
	[Route("api/tasks")]
	public class TasksController: BaseController
    {
		private readonly ITaskService _taskService;

		public TasksController(ITaskService taskService)
		{
			_taskService = taskService;
		}

        // Create Task

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            if (!IsAuthenticated())
                return Unauthorized("UnAuthorized accesss");
            return Ok(await _taskService.CreateTaskAsync(dto, UserId!));
        }

        // Get Task Own

        [HttpGet]
        public async Task<IActionResult> Get(int? page, int? pageSize)
        {
            if (!IsAuthenticated())
                return Unauthorized("UnAuthorized accesss");
            return Ok(await _taskService.GetTaskAsync(UserId!,IsAdmin(),page,pageSize));
        }


        // Update Task
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
        {
            if (!IsAuthenticated())
                return Unauthorized("UnAuthorized accesss");

            await _taskService.UpdateTaskAsync(id, dto, UserId!);
            return NoContent();
        }


        // Only admin update status of task
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkCompleted(int id)
        {
            if (IsAdmin()==false)
                return Unauthorized("UnAuthorized accesss");

            await _taskService.MarkCompletedAsync(id);
            return NoContent();
        }
    }
}

