using TaskManager.Api.Models;
namespace TaskManager.Api.Services;
public interface ITaskRepository
{
IEnumerable<TaskItem> GetAll();
TaskItem? GetById(int id);
TaskItem Create(TaskItem task);
bool Update(int id, TaskItem updated);
bool Delete(int id);
}