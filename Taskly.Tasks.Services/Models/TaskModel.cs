using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Taskly.Tasks.Repository.Entities;
using Taskly.Tasks.Shared.Enums;

namespace Taskly.Tasks.Services.Models
{
    public record TaskModel
    {
        public Guid Id { get; init; }
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Title { get; init; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; init; } = string.Empty;
        public bool IsCompleted { get; init; }
        public Guid? UserId { get; init; }
        public string Priority { get; init; } = "Medium";
        public DateTimeOffset? DueDate { get; init; }
        [Required]
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? ModifiedDate { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }

        [JsonIgnore] 
        public bool IsOverdue => DueDate.HasValue && DueDate < DateTimeOffset.UtcNow;

        public static TaskModel? FromEntity(TaskItem? entity)
        {
            return entity == null
                ? null
                : new TaskModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    IsCompleted = entity.IsCompleted,
                    UserId = entity.UserId,
                    Priority = entity.Priority.ToString(),
                    DueDate = entity.DueDate,
                    CreatedAt = entity.CreatedAt,
                    ModifiedDate = entity.ModifiedDate,
                    CompletedAt = entity.CompletedAt
                };
        }

        public TaskItem ToEntity()
        {
            return new TaskItem
            {
                Id = Id,
                Title = Title,
                Description = Description,
                IsCompleted = IsCompleted,
                UserId = UserId,
                Priority = Enum.Parse<TaskPriority>(Priority, true),
                DueDate = DueDate,
                CompletedAt = IsCompleted ? CompletedAt ?? DateTimeOffset.UtcNow : null
            };
        }
    }
}

