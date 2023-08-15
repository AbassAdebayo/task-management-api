using System.ComponentModel.DataAnnotations;
using MassTransit;

public class TaskDto
{
    public Guid Id { get; set; } = NewId.Next().ToGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

}

public class CreateTaskRequestModel
{
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 5)]
    public string Title { get; set; }

    [Required]
    [StringLength(maximumLength: 200, MinimumLength = 10)]
    public string Description { get; set; }


}

public class UpdateTaskStatusRequestModel
{
   [Required]
    public string Status { get; set; }

}

