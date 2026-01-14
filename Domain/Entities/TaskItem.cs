using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; } = DateTime.UtcNow;

        public string OwnerUserId { get; set; } = null!;
    }
}

