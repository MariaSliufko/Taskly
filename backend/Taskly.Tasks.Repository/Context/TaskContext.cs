using Microsoft.EntityFrameworkCore;
using Taskly.Tasks.Repository.Entities;

namespace Taskly.Tasks.Repository.Context
{
    public class TaskContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.ModifiedDate = DateTimeOffset.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = DateTimeOffset.UtcNow;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // IsRowVersion is used to implement optimistic concurrency control.
            // This ensures the database tracks changes using a timestamp (RowVersion).
            // If a conflict occurs (e.g., two users try to update the same row simultaneously),
            // the operation will fail instead of silently overwriting the data.
            modelBuilder.Entity<TaskItem>().Property(t => t.RowVersion).IsRowVersion();
        }
    }
}

