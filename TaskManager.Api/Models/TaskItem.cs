using System.ComponentModel.DataAnnotations;
namespace TaskManager.Api.Models;
public class TaskItem
{
public int Id { get; set; }
[Required]
[StringLength(100, MinimumLength = 3)]
public string Title { get; set; } = string.Empty;
[StringLength(500)]
public string? Description { get; set; }
public bool IsCompleted { get; set; }
public TaskPriority Priority { get; set; } = TaskPriority.Medium;
public DateTime? DueDate { get; set; }
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}