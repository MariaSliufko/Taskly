using System.ComponentModel.DataAnnotations;

namespace Taskly.Tasks.Repository.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = [];
    }
}
