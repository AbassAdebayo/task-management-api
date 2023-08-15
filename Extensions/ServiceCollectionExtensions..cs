using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
        
        .AddScoped<ITaskRepository, TaskRepository>()
        .AddScoped<ITaskService, TaskService>();
    
                        
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TaskContext>(options =>
            options.UseMySQL(connectionString));

        return services;
    }
}