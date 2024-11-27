using System.ComponentModel.DataAnnotations;
using Taskly.Tasks.Shared.Enums;

namespace Taskly.Tasks.Services.Models
{
    public class CreateTaskRequest
    {
        [Required]
        [MinLength(3)]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; } 

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }
    }
}
