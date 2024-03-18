using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services;
using Task = WebApplication2.Models.Task;

namespace WebApplication2.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string title="", int page = 1, int limit = 10)
        {            
            var result = await _taskService.getTaskByTitleAndOffset(title, page, limit);
            var TaskList = result.Item1;
            var totalPage = result.Item2;
            ViewBag.titleFilter = title;
            ViewBag.page = page;
            ViewBag.limit = limit;
            ViewBag.totalPage = totalPage;            
            return View(TaskList);
        }      

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskService.getTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TaskId,TaskTitle,TaskStatus,TaskDescription,TaskDueDate")] Models.Task task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.addTask(task);
                    return RedirectToAction(nameof(Index));
                }
                return View(task);
            } catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskService.getTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskTitle,TaskStatus,TaskDescription,TaskDueDate")] Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _taskService.updateTask(task);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_taskService.TaskExists(task.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskService.getTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var task = await _taskService.getTaskById(id);
                _taskService.deleteTask(task);
                return RedirectToAction(nameof(Index));
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
