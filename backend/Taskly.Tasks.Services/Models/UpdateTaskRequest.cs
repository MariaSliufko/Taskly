using System.ComponentModel.DataAnnotations;
using Taskly.Tasks.Shared.Enums;

namespace Taskly.Tasks.Services.Models
{
    public class UpdateTaskRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public bool IsCompleted { get; set; }
        public DateTimeOffset? DueDate { get; set; }
    }
}
