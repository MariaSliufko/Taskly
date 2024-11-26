using Taskly.Tasks.Repository.Context;
using Taskly.Tasks.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Taskly.Tasks.Repository.Entities;
using Taskly.Tasks.Shared.Models;

namespace Taskly.Tasks.Repository.Implementation
{
    public class TaskRepository(TaskContext context) : ITaskRepository
    {
        private readonly TaskContext _context = context;

        public async Task<(int total, List<TaskItem>)> GetTasksAsync(TaskQueryParameters parameters, CancellationToken ct = default)
        {
            var query = _context.Tasks.AsNoTracking();

            if (!string.IsNullOrEmpty(parameters.Search))
            {
                query = query.Where(t => t.Title.Contains(parameters.Search));
            }

            query = parameters.SortBy switch
            {
                "Priority" => query.OrderBy(t => t.Priority),
                _ => query.OrderBy(t => t.CreatedAt)
            };

            var totalCount = await query.CountAsync(ct);
            var tasks = await query.Skip(parameters.Skip).Take(parameters.Take).ToListAsync(ct);

            return (totalCount, tasks);
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, ct);

        }

        public async Task AddTaskAsync(TaskItem task, CancellationToken ct = default)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateTaskAsync(TaskItem task, CancellationToken ct = default)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteTaskAsync(Guid id, CancellationToken ct = default)
        {
            var task = await _context.Tasks.FindAsync(new object[] { id }, ct);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
