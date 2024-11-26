using Microsoft.Extensions.Logging;
using Taskly.Tasks.Repository.Contract;
using Taskly.Tasks.Repository.Entities;
using Taskly.Tasks.Services.Contract;
using Taskly.Tasks.Services.Models;
using Taskly.Tasks.Shared.Models;

namespace Taskly.Tasks.Services.Implementation
{
    public class TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger) : ITaskService
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly ILogger<TaskService> _logger = logger;

        public async Task<PagedResponse<List<TaskModel>>> GetTasksAsync(TaskQueryParameters parameters, CancellationToken ct = default)
        {
            try
            {
                var (totalCount, taskEntities) = await _taskRepository.GetTasksAsync(parameters, ct);

                var taskModels = taskEntities.Select(task => new TaskModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority.ToString(),
                    DueDate = task.DueDate,
                    IsCompleted = task.IsCompleted,
                    CreatedAt = task.CreatedAt
                }).ToList();

                return new PagedResponse<List<TaskModel>>(taskModels, totalCount, parameters.Skip / parameters.Take + 1, parameters.Take);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching tasks.");
                throw;
            }
        }

        public async Task<TaskModel?> GetTaskByIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var taskEntity = await _taskRepository.GetTaskByIdAsync(id, ct);

                if (taskEntity == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found.", id);
                    return null;
                }

                var taskModel = new TaskModel
                {
                    Id = taskEntity.Id,
                    Title = taskEntity.Title,
                    Description = taskEntity.Description,
                    Priority = taskEntity.Priority.ToString(),
                    DueDate = taskEntity.DueDate,
                    IsCompleted = taskEntity.IsCompleted,
                    CreatedAt = taskEntity.CreatedAt
                };

                return taskModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching task with ID {TaskId}.", id);
                throw;
            }
        }

        public async Task<Guid> CreateTaskAsync(CreateTaskRequest request, CancellationToken ct = default)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogError("CreateTaskAsync was called with a null request.");
                    throw new ArgumentNullException(nameof(request));
                }

                var taskItem = new TaskItem
                {
                    Title = request.Title,
                    Description = request.Description ?? string.Empty,
                    Priority = request.Priority,
                    DueDate = request.DueDate,
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsCompleted = false
                };

                await _taskRepository.AddTaskAsync(taskItem, ct);
                _logger.LogInformation("Task with ID {TaskId} was successfully created.", taskItem.Id);

                return taskItem.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a task.");
                throw;
            }
        }

        public async Task UpdateTaskAsync(Guid id, UpdateTaskRequest request, CancellationToken ct = default)
        {
            try
            {
                var task = await _taskRepository.GetTaskByIdAsync(id, ct) ??
                           throw new KeyNotFoundException($"Task with ID {id} not found.");

                task.Title = request.Title;
                task.Description = request.Description;
                task.Priority = request.Priority;
                task.DueDate = request.DueDate;

                await _taskRepository.UpdateTaskAsync(task, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating task with ID {Id}", id);
                throw;
            }
        }

        public async Task DeleteTaskAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var task = await _taskRepository.GetTaskByIdAsync(id, ct) ?? throw new KeyNotFoundException($"Task with ID {id} not found.");
                await _taskRepository.DeleteTaskAsync(id, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting task with ID {Id}", id);
                throw;
            }
        }
    }
}
