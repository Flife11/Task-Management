using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using Task = WebApplication2.Models.Task;

namespace WebApplication2.Services
{
    public class TaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task< List<Task> > getAllTask()
        {
            var TaskList = await _context.Task.ToListAsync();
            return TaskList;
        }

        public async Task<Tuple<List<Task>, int>> getTaskByTitleAndOffset(string title="", int page = 0, int limit = 10)
        {
            var offset = (page-1) * limit;
            var totalRecord = _context.Task.Where(x => x.TaskTitle.ToLower().Contains(title.ToLower())).Count();
            var totalPage = (totalRecord / limit) + (totalRecord % limit ==  0 ? 0 : 1);
            var TaskList = await _context.Task.Where(x => x.TaskTitle.ToLower().Contains(title.ToLower()))
                                                  .Skip(offset)
                                                  .Take(limit)
                                                  .ToListAsync();            
            return Tuple.Create(TaskList, totalPage);
        }

        public async Task<Task> getTaskById(int? id)
        {
            var task = await _context.Task
                .FirstOrDefaultAsync(m => m.TaskId == id);
            return task;
        }

        public void addTask(Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        public void updateTask(Task task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public async void deleteTask(Task task)
        {
            if (task != null)
            {
                _context.Task.Remove(task);
            }

            await _context.SaveChangesAsync();
        }

        public bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskId == id);
        }
    }
}
