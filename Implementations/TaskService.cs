public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<BaseResponse<bool>> CreateTask(CreateTaskRequestModel model)
    {
        // Check if task already exist
        var checkIFTaskExist = await _taskRepository.TaskExistsByTitleAndDescriptionAsync(model.Title, model.Description);
        if(checkIFTaskExist) return new BaseResponse<bool> {Message = $"Task with title {model.Title} and description {model.Description} already exist!", 
            Status = false

        };

        // Check if model (input) is not null
        if (model == null)
        {
                throw new ArgumentNullException(nameof(model));
        }

        // If the task does not exist and the model is not null, create new task
        var task = new Task
        {
            Title = model.Title,
            Description = model.Description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };
       var newTask = await _taskRepository.CreateTask(task);

       // Check if task was created successfully
       if(newTask is null) return new BaseResponse<bool> {Message = "Task creation unsuccessful", Status = false};

       // Log a message for the event
       _logger.LogWarning($"A new Task titled '{task.Title}' has been created successfully");
       return new BaseResponse<bool> 
       {
            Message = $"A new Task titled '{task.Title}' has been created successfully and the " + 
            $"status has been set to {task.Status}",
            Status = true,
       };
    }

    public async Task<BaseResponse<bool>> DeleteTask(Guid taskId)
    {
       var getTask = await _taskRepository.GetTaskById(taskId);
        if(getTask is null) return new BaseResponse<bool> {Message = $"Task with Id {taskId} doesn't exist", Status = false};

         await _taskRepository.DeleteTask(taskId);
         _logger.LogWarning($"The task with Id {taskId} has been deleted successfully");
        return new BaseResponse<bool> 
       {
            Message = $"The task with Id {taskId} has been deleted successfully",
            Status = true,
       };
    }

    public async Task<IEnumerable<Task>> GetAllTasksAsync()
    { 
      var tasks = await _taskRepository.GetAllTasksAsync();
      return tasks.Select(task => new Task
      {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status,
        CreatedAt = task.CreatedAt,
        UpdatedAt  = task.UpdatedAt
    

      });
    }

    public async Task<BaseResponse<Task>> GetTaskById(Guid taskId)
    {
        // Fetch task
        var getTask = await _taskRepository.GetTaskById(taskId);
        if(getTask is null)
        {
               
            return new BaseResponse<Task>
            {
                Message = $"Task with Id {taskId} doesn't exist",
                Status  = false
            };
        }
         _logger.LogWarning($"Task with Id '{taskId}' fetched successfully");
        return new BaseResponse<Task>
        {
            Status = true,
            Data = new Task
            {
                Id = getTask.Id,
                Title = getTask.Title,
                Description = getTask.Description,
                Status = getTask.Status,
                CreatedAt = getTask.CreatedAt,
                UpdatedAt = getTask.UpdatedAt

            },
            Message = $"Task with Id '{taskId}' fetched successfully.",
        };
    }

    public async Task<BaseResponse<bool>> UpdateTaskStatus(Guid taskId, UpdateTaskStatusRequestModel model)
    {
        // Fetch task
        var fetchTask = await _taskRepository.GetTaskById(taskId);

        // Check if task with the provided is not null
        if(fetchTask is null) return new BaseResponse<bool> {Message = $"Task with Id {taskId} doesn't exist", Status = false};

        // Check if input is not null
        if(model.Status is null) return new BaseResponse<bool> {Message = $"Field cannot be left empty", Status = false};

        // Check if the returning Status is different from the new Status
        if(fetchTask.Status == model.Status) return new BaseResponse<bool> {Message = $"The returning status is the same with the new one. " + 
        "Kindly change to another status", Status = false};


        // Update task status
        fetchTask.Status = model.Status;
        fetchTask.UpdatedAt = DateTime.UtcNow;
        var update = await _taskRepository.UpdateTaskStatus(fetchTask.Id, fetchTask.Status);

        // Check if task status was updated successfully
       if(update is null) return new BaseResponse<bool> {Message = "Task status update unsuccessful", Status = false};

       // Log a message for the event
       _logger.LogWarning($"The task status with Id {taskId} has been successfully updated to {update.Status}");
       return new BaseResponse<bool> 
       {
            Message = $"The task status with Id {taskId} has been updated to {update.Status} successfully",
            Status = true,
       };
    }
}