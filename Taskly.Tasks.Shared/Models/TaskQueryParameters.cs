namespace Taskly.Tasks.Shared.Models
{
    /// <summary>
    /// Represents query parameters for fetching a paginated list of tasks.
    /// </summary>
    public class TaskQueryParameters
    {
        /// <summary>
        /// The number of items to take per page. Default is 10.
        /// </summary>
        public int Take { get; set; } = 10;

        /// <summary>
        /// The number of items to skip. Default is 0.
        /// </summary>
        public int Skip { get; set; } = 0;

        /// <summary>
        /// The search string to filter tasks by title.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// The field to sort tasks by. Default is "CreatedAt".
        /// </summary>
        public string SortBy { get; set; } = "CreatedAt";
    }
}
