using Taskly.Tasks.Services.Contract;
using Taskly.Tasks.Services.Models;
using Taskly.Tasks.Shared.Models;

namespace Taskly.Tasks.Host.Endpoints
{
    public static class TaskEndpoints
    {
        private const string Prefix = "tasks";

        public static void MapTasksEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(Prefix).WithTags("Task");

            group.MapGet("/list", async ([AsParameters] TaskQueryParameters parameters, CancellationToken cancellationToken, ITaskService taskService) =>
            {
                var pagedTasks = await taskService.GetTasksAsync(parameters, cancellationToken);

                return pagedTasks != null && pagedTasks.Data.Count > 0
                    ? Results.Ok(pagedTasks)
                    : Results.NoContent();
            })
                 .WithDisplayName("Gets Tasks")
                 .WithDescription("Fetches a list of tasks with optional pagination, filtering, and sorting parameters.")
                 .Produces<PagedResponse<List<TaskModel>>>(StatusCodes.Status200OK)
                 .Produces(StatusCodes.Status204NoContent)
                 .ProducesProblem(StatusCodes.Status500InternalServerError)
                 .WithOpenApi();


            group.MapGet("/{id:guid}", async (Guid id, ITaskService taskService, CancellationToken ct) =>
            {
                var task = await taskService.GetTaskByIdAsync(id, ct);

                return task != null
                    ? Results.Ok(task)
                    : Results.NotFound(new { Message = $"Task with ID {id} not found." });
            })
                .WithName("GetTaskById")
                .WithDisplayName("Get Task by ID")
                .WithDescription("Fetches a specific task by its ID.")
                .Produces<TaskModel>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithOpenApi();


            group.MapPost("/create", async (CreateTaskRequest request, ITaskService taskService, CancellationToken cancellationToken) =>
            {
                var taskId = await taskService.CreateTaskAsync(request, cancellationToken);
                return Results.Created($"{Prefix}/{taskId}", taskId);
            })
                .WithName("CreateTask")
                .WithDisplayName("Create Task")
                .WithDescription("Creates a new task.")
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithOpenApi();

            group.MapPut("/{id:guid}", async (Guid id, UpdateTaskRequest request, ITaskService taskService, CancellationToken ct) =>
            {
                await taskService.UpdateTaskAsync(id, request, ct);
                return Results.NoContent();
            })
                .WithName("UpdateTask")
                .WithDisplayName("Update Task")
                .WithDescription("Updates an existing task.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithOpenApi();


            group.MapDelete("/{id:guid}", async (Guid id, ITaskService taskService, CancellationToken ct) =>
            {
                await taskService.DeleteTaskAsync(id, ct);
                return Results.NoContent();
            })
                .WithName("DeleteTask")
                .WithDisplayName("Delete Task")
                .WithDescription("Deletes a task by its ID.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithOpenApi();
        }
    }
}
