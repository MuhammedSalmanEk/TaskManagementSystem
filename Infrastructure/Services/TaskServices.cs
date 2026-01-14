using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskManagement.Domain.Entities;

namespace Infrastructure.Services
{
	public class TaskServices:ITaskService
	{
		private readonly AppDbContext _context;

		public TaskServices(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                OwnerUserId = userId
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt.ToLocalTime(),
                DueDate=task.DueDate?.ToLocalTime(),
                Description=task.Description,
                UserId=task.OwnerUserId
                
            };
           
        }

        public async Task<IEnumerable<TaskResponseDto>> GetTaskAsync(string userId, bool isAdmin, int? page,int? pageSize)
        {
            var query = isAdmin ? _context.Tasks : _context.Tasks.Where(t => t.OwnerUserId == userId);
            int currentPage = page ?? 1;
            int size = pageSize ?? 10;
            var tasks= await query.Skip((currentPage - 1) * size).Take(size).ToListAsync();
            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt.ToLocalTime(),
                Description = t.Description,
                UserId =t.OwnerUserId,
                DueDate = t.DueDate?.ToLocalTime(),

            }).ToList();
        }

        public async Task MarkCompletedAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                throw new Exception("Task not found");

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

        }

        public async Task UpdateTaskAsync(int id, UpdateTaskDto dto,string userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerUserId == userId);

            if (task == null)
            {
                throw new Exception("Task Not found");
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;

            await _context.SaveChangesAsync();
        }
    }
}

