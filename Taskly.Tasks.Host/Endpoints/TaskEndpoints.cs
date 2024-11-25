namespace Taskly.Tasks.Host.Endpoints
{
    public static class TaskEndpoints
    {
        private const string Prefix = "tasks";

        public static void MapTasksEnpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(Prefix).WithTags("Task");

            group.MapGet("/", () =>
            {
                return Results.Ok(new List<object>
                {
                    new { Id = Guid.NewGuid(), Title = "Testa Minimal API", Priority = "High", IsCompleted = false },
                    new { Id = Guid.NewGuid(), Title = "Lär dig Minimal API", Priority = "Medium", IsCompleted = false }
                });
            }).WithName("GetAllTasks")
            .WithDisplayName("Get Tasks")
            .WithDescription("Gets a list of all tasks.")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        }
    }
}
