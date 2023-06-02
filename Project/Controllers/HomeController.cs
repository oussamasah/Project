using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Project.Areas.Identity.Data;
using Project.Data;
using Project.Models;
using Project.Models.Form;
using System.Diagnostics;

namespace Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly UserManager<AppplicationUser> _userManager;

        public readonly IToastNotification _notif;
        public readonly AuhtDbContext _db;
        public HomeController(AuhtDbContext context,ILogger<HomeController> logger,UserManager<AppplicationUser> userManager, IToastNotification n)
        {
            _logger = logger;
            _userManager = userManager;
            _db=context;    
            _notif=n;    
        }

        public async Task<IActionResult> Index()
        {
        
           var projects = await _db.project.ToListAsync();
            return View(projects);
        }
        [HttpGet]
        public IActionResult Add()
        {           
            return View();
        }
        




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProjectForm addProjectReq)
        {
            
            if (!ModelState.IsValid)
            {
                return View(addProjectReq);
            }

            var p = new Projects
            {
                Name = addProjectReq.Name
            };
            _db.project.Add(p); 
            _db.SaveChanges();
           _notif.AddSuccessToastMessage("Project added successfuly");

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int?id) {
            if(id == null)
            {
                return BadRequest(ModelState);

            }

            var project = await _db.project.FindAsync(id);
            if (project == null)
            {
                return NotFound();

            }
            var viewModel = new ProjectForm
            {
                Id = project.Id,
                Name = project.Name
            };
                return View("Add", viewModel);
            }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectForm p) {
            if (!ModelState.IsValid)
            {
                return View("Add", p);
            }

            var project = await _db.project.FindAsync(p.Id);
        //    if(project == null)
       //      return NotFound();
            
            project.Name = p.Name;
            _db.SaveChanges();

            _notif.AddSuccessToastMessage("Project updated successfuly");
             return RedirectToAction(nameof(Index));
            }






        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(ModelState);

            }

            var project = await _db.project.FindAsync(id);
            if (project == null)
            {
                return NotFound();

            }
            _db.project.Remove(project);
            _db.SaveChanges();  
            return Ok();
        }









        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}