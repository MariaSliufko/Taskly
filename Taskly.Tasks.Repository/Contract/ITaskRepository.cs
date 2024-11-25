using Taskly.Tasks.Repository.Entities;

namespace Taskly.Tasks.Repository.Contract
{
    internal interface ITaskRepository
    {
        Task<(int total, List<TaskItem>)> GetTasksAsync(int take, int skip, CancellationToken ct = default);
        Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct = default);
        Task AddTaskAsync(TaskItem task, CancellationToken ct = default);
        Task UpdateTaskAsync(TaskItem task, CancellationToken ct = default);
        Task DeleteTaskAsync(Guid id, CancellationToken ct = default);
    }
}
