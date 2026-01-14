using System;
using Application.DTOs;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services
{
	public class TaskServiceTests
	{
		private AppDbContext GetAppDbContext()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

			return new AppDbContext(options);
		}


		[Fact]
		public async Task CreateTaskAsync_ShouldCreateTask()
		{
			var context = GetAppDbContext();

			var service = new TaskServices(context);

			var dto = new CreateTaskDto
			{
				Title = "Test Task",
                Description = "Test description"
            };

			var result = await service.CreateTaskAsync(dto,"user");

			Assert.NotNull(result);
			Assert.Equal("Test Task", result.Title);
			Assert.False(result.IsCompleted);

        }
	}
}

