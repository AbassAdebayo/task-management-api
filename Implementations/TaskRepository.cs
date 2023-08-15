using Microsoft.EntityFrameworkCore;

public class TaskRepository : ITaskRepository
{
    private readonly TaskContext _taskContext;

    public TaskRepository(TaskContext taskContext)
    {
        _taskContext = taskContext;
    }

    public async Task<Task> CreateTask(Task task)
    {
        await _taskContext.Tasks.AddAsync(task);
        await _taskContext.SaveChangesAsync();
        return task;

    }

    public async Task<bool> DeleteTask(Guid taskId)
    {
        var task  = await _taskContext.Tasks.FirstOrDefaultAsync(id => id.Id == taskId);
          _taskContext.Tasks.Remove(task);
          await _taskContext.SaveChangesAsync();
          return true;
    }

    public async Task<IEnumerable<Task>> GetAllTasksAsync()
    {
        return await _taskContext.Tasks.Select(t => new Task
        {
            CreatedAt = t.CreatedAt,
            Id = t.Id,
            Description = t.Description,
            Status = t.Status,
            Title = t.Title
        }).OrderBy(t => t.Title)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<Task> GetTaskById(Guid taskId)
    {
        return 
        await _taskContext.Tasks.FirstOrDefaultAsync(id => id.Id == taskId);
    }

    public async Task<bool> TaskExistsByTitleAndDescriptionAsync(string title, string description)
    {
        return await _taskContext
                .Tasks.AnyAsync(task => task.Title == title && task.Description == description);
    }

    public async Task<Task> UpdateTaskStatus(Guid taskId, string status)
    {
        var getId = await _taskContext.Tasks.FirstOrDefaultAsync(id => id.Id == taskId);
        getId.Status = status;
        _taskContext.Tasks.Update(getId);
        await _taskContext.SaveChangesAsync();
        return getId;

    }
}