using System.ComponentModel.DataAnnotations;
using MassTransit;

public class Task
{
    public Guid Id { get; set; } = NewId.Next().ToGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime UpdatedAt { get; set; }
}