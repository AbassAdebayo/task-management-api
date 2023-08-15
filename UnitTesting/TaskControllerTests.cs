using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class TaskControllerTests
{
    private readonly Mock<TaskService> _taskServiceMock;
    private readonly Mock<ILogger<TaskController>> _loggerMock;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _taskServiceMock = new Mock<TaskService>();
        _loggerMock = new Mock<ILogger<TaskController>>();
        _controller = new TaskController(_taskServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async void GetTasks_ShouldReturnListOfTasks()
    {
        // Arrange
        var tasks = new List<Task>
        {
            new Task { Id = Guid.NewGuid(), Title = "Task 1", Description = "Description 1", Status = "Pending" },
            new Task { Id = Guid.NewGuid(), Title = "Task 2", Description = "Description 2", Status = "In Progress" },
            new Task { Id = Guid.NewGuid(), Title = "Task 3", Description = "Description 3", Status = "Completed" }
        };

        _taskServiceMock.Setup(s => s.GetAllTasksAsync()).Returns(tasks.AsEnumerable<Task>);

        // Act
        var result = await _controller.List() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<Task>>(result.Value);
        Assert.Equal(tasks, returnedTasks.ToList());
    }

    [Fact]
    public async void GetTasks_EmptyTaskList_ShouldReturnEmptyList()
    {
        // Arrange
        var tasks = new List<Task>();
        _taskServiceMock.Setup(s => s.GetAllTasksAsync()).Returns(tasks.AsEnumerable<Task>);

        // Act
        var result = await _controller.List() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<Task>>(result.Value);
        Assert.Empty(returnedTasks);
        
    }

    // [Fact]
    // public async void UpdateTaskStatus_ValidTaskIdAndStatus_ShouldReturnUpdatedTask()
    // {
    //     // Arrange
    //     Guid taskId = Guid.NewGuid();
    //     var newStatus =  new UpdateTaskStatusRequestModel {Status = "In Progress"};
    //     var originalTask = new Task { Id = taskId, Title = "Task 1", Description = "Description 1", Status = "Pending", CreatedAt = DateTime.UtcNow };
    //     var updatedTask = new Task { Id = taskId, Title = "Task 1", Description = "Description 1", Status = newStatus, UpdatedAt = DateTime.UtcNow };

    //     var response = new BaseResponse<Task> { Data = updatedTask, Message = "Task status updated successfully."};
    //     await _taskServiceMock.Setup(s => s.GetTaskById(taskId)).Returns(new BaseResponse<Task> {Data = originalTask});
    //     await _taskServiceMock.Setup(s => s.UpdateTaskStatus(taskId, newStatus)).Returns(new BaseResponse<bool> {Status = true});

    //     // Act
    //     var result = await _controller.StatusUpdate(taskId, newStatus) as OkObjectResult;

    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(updatedTask, result.Value);
    // }

    // [Fact]
    // public async void CreateTask_ValidTaskData_ShouldReturnNewlyCreatedTask()
    // {
    //     // Arrange
    //     var newTask = new CreateTaskRequestModel { Title = "New Task", Description = "New Description" };
    //     var createdTask = new Task { Id = Guid.NewGuid(), Title = "New Task", Description = "New Description", Status = "Pending", CreatedAt = DateTime.UtcNow };

    //     _taskServiceMock.Setup(s => s(newTask)).Returns(createdTask);

    //     // Act
    //     var result = await _controller.CreateTask(newTask) as OkObjectResult;

    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(createdTask, result.Value);
    // }
    
    // [Fact]
    // public async void DeleteTask_ValidTaskId_ShouldReturnNoContent()
    // {
    //     // Arrange
    //     Guid taskId = Guid.NewGuid();
    //     var task = new Task { Id = taskId, Title = "Task 1", Description = "Description 1", Status = "Pending", CreatedAt = DateTime.UtcNow };
    //     var response  = new BaseResponse<Task> {Data = task};
    //     _taskServiceMock.Setup(s => s.GetTaskById(taskId)).Returns(task);

    //     // Act
    //     var result = await _controller.DeleteTask(taskId) as NoContentResult;

    //     // Assert
    //     Assert.NotNull(result);
    //     _taskServiceMock.Verify(s => s.DeleteTask(taskId), Times.Once);
    // }

    //  [Fact]
    // public async void GetTaskById_ValidTaskId_ShouldReturnTask()
    // {
    //     // Arrange
    //     Guid taskId = Guid.NewGuid();
    //     var task = new Task { Id = taskId, Title = "Task 1", Description = "Description 1", Status = "Pending", CreatedAt = DateTime.UtcNow };

    //    await _taskServiceMock.Setup(s => s.GetTaskById(taskId)).Returns(task);

    //     // Act
    //     var result = await _controller.Get(taskId) as OkObjectResult;

    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(task, result.Value);
    // }
}