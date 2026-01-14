using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
namespace Infrastructure.Data
{
    public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :
			base(options)
		{ }

		public DbSet<TaskItem> Tasks { get; set; }
        //public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }
}

