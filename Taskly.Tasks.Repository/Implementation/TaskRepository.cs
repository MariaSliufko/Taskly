using Taskly.Tasks.Repository.Context;
using Taskly.Tasks.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Taskly.Tasks.Repository.Entities;

namespace Taskly.Tasks.Repository.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;
        
        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<(int total, List<TaskItem>)> GetTasksAsync(int take, int skip, CancellationToken ct = default)
        {
            var totalCount = await _context.Tasks.AsNoTracking().CountAsync(ct);
            var tasks = await _context.Tasks.AsNoTracking()
                .OrderBy(t => t.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
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
