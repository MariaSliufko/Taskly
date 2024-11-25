using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskly.Tasks.Shared.Enums;

namespace Taskly.Tasks.Repository.Entities
{
    [Table("TaskItems")]
    public sealed class TaskItem : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTimeOffset? CompletedAt { get; set; }

        public Guid? UserId { get; set; }

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// Updates the completion status and ensures CompletedAt is consistent.
        /// </summary>
        /// <param name="isCompleted">The completion status of the task.</param>
        public void UpdateCompletionStatus(bool isCompleted)
        {
            if (IsCompleted == isCompleted)
            {
                return; 
            }

            IsCompleted = isCompleted;
            CompletedAt = isCompleted ? DateTimeOffset.UtcNow : null;
        }
    }
}
