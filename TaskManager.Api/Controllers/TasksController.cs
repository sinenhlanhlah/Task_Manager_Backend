using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
namespace TaskManager.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
private readonly ITaskRepository _repository;
public TasksController(ITaskRepository repository)
{
_repository = repository;
}
// GET /api/tasks
[HttpGet]
public ActionResult<IEnumerable<TaskItem>> GetAll([FromQuery] bool? completed)
{
var tasks = _repository.GetAll();
if (completed.HasValue)
{
tasks = tasks.Where(t => t.IsCompleted == completed.Value);
}
return Ok(tasks);
}
// GET /api/tasks/5
[HttpGet("{id:int}")]
public ActionResult<TaskItem> GetById(int id)
{var task = _repository.GetById(id);
if (task is null) return NotFound();
return Ok(task);
}
// POST /api/tasks
[HttpPost]
public ActionResult<TaskItem> Create([FromBody] TaskItem task)
{
var created = _repository.Create(task);
return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
}
// PUT /api/tasks/5
[HttpPut("{id:int}")]
public IActionResult Update(int id, [FromBody] TaskItem task)
{
var updated = _repository.Update(id, task);
if (!updated) return NotFound();
return NoContent();
}
// DELETE /api/tasks/5
[HttpDelete("{id:int}")]
public IActionResult Delete(int id)
{
var deleted = _repository.Delete(id);
if (!deleted) return NotFound();
return NoContent();
}
}