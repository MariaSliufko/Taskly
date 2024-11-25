using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taskly.Tasks.Repository.Context;
using Taskly.Tasks.Repository.Contract;
using Taskly.Tasks.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Taskly.Tasks.Repository.Registration
{
    public static class Registration
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var db = configuration.GetSection("Tasks:DataBase");
            var connection = BuildConnectionString(db, configuration);

            ArgumentException.ThrowIfNullOrEmpty(connection, nameof(connection));

            services.AddDbContext<TaskContext>(options =>
            {
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
                options.EnableSensitiveDataLogging(false); 
                options.EnableDetailedErrors(); 
            });

            services.AddScoped<ITaskRepository, TaskRepository>();
        }

        private static string BuildConnectionString(IConfigurationSection db, IConfiguration configuration)
        {
            var user = configuration["Tasks:DataBase:User"];
            var password = configuration["Tasks:DataBase:Password"];

            return $"Server={db["Server"]};Database={db["Name"]};Uid={user};Pwd={password};Port={db["Port"]};";
        }
    }

}
