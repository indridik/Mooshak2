using Mooshak2.Models;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> CreateCourse(Course model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var exists = _db.Courses.SingleOrDefault(x => x.Name == model.Name);
            if(exists == null)
            {
                var course = new Course { ID = model.ID, Name = model.Name };
                _db.Courses.Add(course);
                _db.SaveChanges();

                ViewBag.result = "Course successfully created!";
                return View();
            }
            ViewBag.result = "Course already exists!";
            return View(model);

        }
    }
}