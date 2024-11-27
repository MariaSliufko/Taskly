namespace Taskly.Tasks.Services.Models
{
    /// <summary>
    /// Represents a paginated response for API requests. 
    /// This model contains both the data for the current page and metadata about the overall dataset.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in the current page.</typeparam>
    public class PagedResponse<T>(T data, int totalCount, int pageNumber, int pageSize)
    {
        /// <summary>
        /// The data for the current page.
        /// </summary>
        public T Data { get; set; } = data;

        /// <summary>
        /// The total number of items across all pages.
        /// </summary>
        public int TotalCount { get; set; } = totalCount;

        /// <summary>
        /// The current page number, starting from 1.
        /// </summary>
        public int PageNumber { get; set; } = pageNumber;

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize { get; set; } = pageSize;
    }
}
