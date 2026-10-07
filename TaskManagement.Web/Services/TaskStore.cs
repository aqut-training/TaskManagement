using TaskManagement.Web.Models;

namespace TaskManagement.Web.Services;

// Training project: data is stored in memory and resets when the app restarts.
public class TaskStore
{
    private readonly List<TaskItem> _tasks = new();
    private readonly object _sync = new();
    private int _nextId = 1;

    public List<TaskItem> GetAll()
    {
        lock (_sync)
            return _tasks.OrderBy(task => task.Id).Select(task => new TaskItem
            {
                Id = task.Id, Title = task.Title, Description = task.Description,
                IsDone = task.IsDone, CreatedAt = task.CreatedAt
            }).ToList();
    }

    public void Add(string title, string? description)
    {
        lock (_sync)
            _tasks.Add(new TaskItem { Id = _nextId++, Title = title, Description = description, IsDone = false });
    }

    public bool SetDone(int id, bool isDone)
    {
        lock (_sync)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task is null) return false;
            task.IsDone = isDone;
            return true;
        }
    }
}
