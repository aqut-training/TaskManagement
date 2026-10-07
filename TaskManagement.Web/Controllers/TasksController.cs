using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Models;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers;

public class TasksController : Controller
{
    private readonly TaskStore _store;
    public TasksController(TaskStore store) => _store = store;

    public IActionResult Index() => View(_store.GetAll());

    [HttpGet]
    public IActionResult Create() => View(new CreateTaskViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateTaskViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        _store.Add(model.Title.Trim(), model.Description?.Trim());
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetDone(int id, bool isDone)
    {
        if (!_store.SetDone(id, isDone)) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
