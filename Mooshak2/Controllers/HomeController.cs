using Mooshak2.Models;
using Mooshak2.Models.Entities;
using System.Web.Mvc;
using Mooshak2.Models.ViewModels;
using System.Linq;
using Mooshak2.DAL;

namespace Mooshak2.Controllers
{
    public class HomeController : Controller
    {
        private MooshakDataContext context;

        [Authorize]
        public ActionResult Index()
        {
            if(User.IsInRole("Administrators"))
            {
                return View("Admin");
            }
            else if(User.IsInRole("Teachers"))
            {
                return View("Teacher");
            }
            else
            {
               return RedirectToAction("Student");
            }
        }
        
        [Authorize]
        public ActionResult Student()
        {
            var model = context.Students.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Mooshak 2.0";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hópmeðlimir.";

            return View();
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult AssignmentJson(int id)
        {
            var assignments = context.Assignments.Where(x => x.CourseID == id).ToList();
            return Json(assignments, JsonRequestBehavior.AllowGet);
        }
    }
}