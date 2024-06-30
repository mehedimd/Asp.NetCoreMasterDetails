using CoreMasterDetailAwesome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreMasterDetailAwesome.Controllers
{
    public class FacultyController : Controller
    {
        private readonly MyContext context;
        IWebHostEnvironment env;
        public FacultyController(MyContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env; 

        }
        public IActionResult Index()
        {
            var data = context.Faculties.Include(s => s.Students).ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> course = new List<SelectListItem>()
            {
                new SelectListItem(){Value = "Asp.Net", Text = "Asp.Net"},
                new SelectListItem(){Value = "C#", Text = "C#"}

            };
            ViewBag.courses = course;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Faculty faculty)
        {
            faculty.Students.RemoveAll(s=>s.IsDeleted==true);
            faculty.PicPath = GetFileName(faculty);
            context.Add(faculty);
            if (context.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            } ;
            return View(faculty);
        }
        private string GetFileName(Faculty faculty)
        {
            var fileName = "";
            if(faculty.Picture != null)
            {
                var rootFolder = Path.Combine(env.WebRootPath, "Images");
                if(!Directory.Exists(rootFolder))
                {
                    Directory.CreateDirectory(rootFolder);
                }
                fileName = Guid.NewGuid().ToString() + "_" + faculty.Picture.FileName;
                var fileToSave = Path.Combine(rootFolder, fileName);
                using(var fileStream = new FileStream(fileToSave, FileMode.Create))
                {
                    faculty.Picture.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public IActionResult DeleteConfirmation(int id)
        {
            return View(context.Faculties.Include(p => p.Students).Where(f => f.FacultyID == id).FirstOrDefault());
        }
        public IActionResult Delete(int id)
        {
            var faculty =context.Faculties.Find(id);
            context.Faculties.Remove(faculty);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> course = new List<SelectListItem>()
            {
                new SelectListItem(){Value = "Asp.Net", Text = "Asp.Net"},
                new SelectListItem(){Value = "C#", Text = "C#"}

            };
            ViewBag.courses = course;
            var faculty = context.Faculties.Include(p=>p.Students).Where(f=>f.FacultyID == id).FirstOrDefault();

            return View(faculty);
        }
        [HttpPost]
        public IActionResult Edit(Faculty faculty)
        {

            faculty.Students.RemoveAll(s => s.IsDeleted == true);

            List<Student> students = context.Students.Where(s=>s.FacultyID == faculty.FacultyID).ToList();
            context.Students.RemoveRange(students);
            context.SaveChanges();

            if(faculty.Picture != null)
            {
                faculty.PicPath = GetFileName(faculty);
            }

            context.Attach(faculty);
            context.Entry(faculty).State = EntityState.Modified;
            if (context.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            };
            return View(faculty);
        }

    }
}
