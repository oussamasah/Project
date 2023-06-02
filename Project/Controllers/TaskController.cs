using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Project.Data;
using Project.Models;
using Project.Models.Form;

namespace Project.Controllers
{
    public class TaskController : Controller
    {

        public readonly IToastNotification _notif;
        public readonly AuhtDbContext _db;

        public TaskController(AuhtDbContext context, IToastNotification n)
        {
            _db=context;
            _notif=n;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return BadRequest();

            var projects = await _db.project.FindAsync(id);
            if (projects==null)
                return NotFound();

            var tasks =  await _db.task.Where(t=>t.Project == id).ToListAsync();
           
            ViewData["projects"] = projects;
            ViewData["tasks"] = tasks;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
                return BadRequest();

            var projects = await _db.project.FindAsync(id);
            if (projects == null)
                return NotFound();

            ViewData["projects"] = projects;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TaskForm t)
        {


            if (!ModelState.IsValid)
            {
                return View(t);
            }
            var projects = await _db.project.FindAsync(t.Project);
            if (projects == null)
                return NotFound();

            ViewData["projects"] = projects;

            var task = new Tasks
            {
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                Project = t.Project,
            };
           
            _db.task.Add(task);
            _db.SaveChanges();
            _notif.AddSuccessToastMessage("Task added successfuly");

            return RedirectToAction(nameof(Index), new { id = t.Project });
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest(ModelState);

            }

            var task = await _db.task.FindAsync(id);
            if (task == null)
            {
                return NotFound();

            }
            var projects = await _db.project.FindAsync(task.Project);
            if (projects == null)
                return NotFound();

            ViewData["projects"] = projects;

            var viewModel = new TaskForm();


            viewModel.Id = task.Id;
            viewModel.Title = task.Title;
            viewModel.Description = task.Description;
            viewModel.Priority = task.Priority;
            viewModel.Status = task.Status;
            viewModel.Project = task.Project;
            
            return View("Add", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskForm t)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", t);
            }
            var projects = await _db.project.FindAsync(t.Project);
            if (projects == null)
                return NotFound();

            ViewData["projects"] = projects;

            var task = await _db.task.FindAsync(t.Id);
                if(task == null)
                  return NotFound();

            task.Title = t.Title;
            task.Description = t.Description;
            task.Priority = t.Priority;
            _db.SaveChanges();

            _notif.AddSuccessToastMessage("Task updated successfuly");
            return RedirectToAction(nameof(Index),new {id=task.Project});
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }

            var task = await _db.task.FindAsync(id);
            if (task == null)
            {
                return NotFound();

            }
            _db.task.Remove(task);
            _db.SaveChanges();
            return Ok();
        }




    }






}
