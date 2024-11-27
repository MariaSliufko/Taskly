using Taskly.Tasks.Repository.Entities;
using Taskly.Tasks.Shared.Models;

namespace Taskly.Tasks.Repository.Contract
{
    public interface ITaskRepository
    {
        Task<(int total, List<TaskItem>)> GetTasksAsync(TaskQueryParameters parameters, CancellationToken ct = default);
        Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct = default);
        Task AddTaskAsync(TaskItem task, CancellationToken ct = default);
        Task UpdateTaskAsync(TaskItem task, CancellationToken ct = default);
        Task DeleteTaskAsync(Guid id, CancellationToken ct = default);
    }
}
