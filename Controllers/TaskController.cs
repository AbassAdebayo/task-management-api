using System.Net;
using Microsoft.AspNetCore.Mvc;

[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;

    public TaskController(ITaskService taskService, ILogger<TaskController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpPost("createtask")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CreateTaskRequestModel))]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequestModel model)
    {
        var response = await _taskService.CreateTask(model);
        return Ok(response);
    }

    [HttpGet("{taskId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<Task>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([FromRoute] Guid taskId)
    {
        var response = await _taskService.GetTaskById(taskId);
        return response.Status.Equals(true) ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<Task>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> List()
    {
        var response = await _taskService.GetAllTasksAsync();
        if(response.ToList().Count < 1) return BadRequest(response);
        return Ok(response);
    }

    [HttpPut("{taskId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> StatusUpdate([FromRoute] Guid taskId, [FromBody] UpdateTaskStatusRequestModel model)
    {
        var response = await _taskService.UpdateTaskStatus(taskId, model);
        return response.Status.Equals(true) ? Ok(response) : BadRequest(response);
    }

    [HttpPost("{taskId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
    {
        var response = await _taskService.DeleteTask(taskId);
        return response.Status.Equals(true) ? Ok(response) : BadRequest(response);
    }
}
