using Microsoft.Extensions.DependencyInjection;
using Taskly.Tasks.Services.Contract;
using Taskly.Tasks.Services.Implementation;

namespace Taskly.Tasks.Services.Registration
{
    public static class Registration
    {
        public static void AddTaskServices(this IServiceCollection services)
        {
            services.AddTransient<ITaskService, TaskService>();
        }
    }
}
