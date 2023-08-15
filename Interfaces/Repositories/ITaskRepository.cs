public interface ITaskRepository
{
    public Task<Task> CreateTask(Task task);
    public Task<IEnumerable<Task>> GetAllTasksAsync();
    public Task<Task> GetTaskById(Guid taskId);
    public Task<Task> UpdateTaskStatus(Guid taskId, string status);
     public Task<bool> TaskExistsByTitleAndDescriptionAsync(string title, string description);
     public Task<bool> DeleteTask(Guid taskId);

}