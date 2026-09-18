using TaskManager.Api.Models;
namespace TaskManager.Api.Services;
public class InMemoryTaskRepository : ITaskRepository
{
    public InMemoryTaskRepository()
{
Create(new TaskItem { Title = "Set up development environment", IsCompleted = true });
Create(new TaskItem { Title = "Build the Tasks API", Priority = TaskPriority.High });
}
    private readonly List<TaskItem> _tasks = new();
    private readonly object _lock = new();
    private int _nextId = 1;
    public IEnumerable<TaskItem> GetAll()
    {
    lock (_lock)
    {
return _tasks.ToList();
}
}
public TaskItem? GetById(int id)
{
lock (_lock)
{
return _tasks.FirstOrDefault(t => t.Id == id);
}
}public TaskItem Create(TaskItem task)
{
lock (_lock)
{
task.Id = _nextId++;
task.CreatedAt = DateTime.UtcNow;
_tasks.Add(task);
return task;
}
}
public bool Update(int id, TaskItem updated)
{
lock (_lock)
{
var existing = _tasks.FirstOrDefault(t => t.Id == id);
if (existing is null) return false;
existing.Title = updated.Title;
existing.Description = updated.Description;
existing.IsCompleted = updated.IsCompleted;
existing.Priority = updated.Priority;
existing.DueDate = updated.DueDate;
return true;
}
}
public bool Delete(int id)
{
lock (_lock)
{
var existing = _tasks.FirstOrDefault(t => t.Id == id);
if (existing is null) return false;
_tasks.Remove(existing);
return true;
}
}
}