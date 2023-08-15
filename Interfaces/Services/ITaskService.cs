public interface ITaskService
{
    public Task<BaseResponse<bool>> CreateTask(CreateTaskRequestModel model);
    public Task<IEnumerable<Task>> GetAllTasksAsync();
    public Task<BaseResponse<Task>> GetTaskById(Guid taskId);
    public Task<BaseResponse<bool>> UpdateTaskStatus(Guid taskId, UpdateTaskStatusRequestModel model);
    public Task<BaseResponse<bool>> DeleteTask(Guid taskId);
}