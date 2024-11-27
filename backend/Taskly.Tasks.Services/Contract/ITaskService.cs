using Taskly.Tasks.Services.Models;
using Taskly.Tasks.Shared.Models;

namespace Taskly.Tasks.Services.Contract
{
    public interface ITaskService
    {
        Task<PagedResponse<List<TaskModel>>> GetTasksAsync(TaskQueryParameters parameters, CancellationToken ct = default);
        Task<TaskModel?> GetTaskByIdAsync(Guid id, CancellationToken ct = default);
        Task<Guid> CreateTaskAsync(CreateTaskRequest request, CancellationToken ct = default);
        Task UpdateTaskAsync(Guid id, UpdateTaskRequest request, CancellationToken ct = default);
        Task DeleteTaskAsync(Guid id, CancellationToken ct = default);
    }
}

