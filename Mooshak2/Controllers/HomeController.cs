using Mooshak2.Models;
using Mooshak2.Models.Entities;
using System.Web.Mvc;
using Mooshak2.Models.ViewModels;
using System.Linq;
using Mooshak2.DAL;
using Mooshak2.Services;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Mooshak2.Controllers
{
    public class HomeController : Controller
    {
        private MooshakDataContext context = new MooshakDataContext();
        private CourseService cService = new CourseService();
        private AssignmentsService aService = new AssignmentsService();
        private StudentService sService = new StudentService();
        private TeacherService tService = new TeacherService();
        
        [Authorize]
        public ActionResult Index()
        {

            if (User.IsInRole("Administrators"))
            {
                return View("Admin");
            }
            else if(User.IsInRole("Teachers"))
            {

                return RedirectToAction("Teacher");
            }
            else
            {
               return RedirectToAction("Student");
            }
        }
        
        [DoNotAuthorize(Roles ="Teachers")]
        public ActionResult Student()
        {
            IdentityManager manager = new IdentityManager();
            string id = User.Identity.GetUserId();
            var courses = cService.GetAllCourses();
            if (!(manager.UserIsInRole(id, "Administrators")))
            {
                string studentName = AuthenticationManager.User.Identity.Name;

                int Id = sService.GetStudentIdByName(studentName);

                courses = cService.GetAllCoursesForStudent(Id);
            }

            List<CourseViewModel> model = new List<CourseViewModel>();
            foreach(var course in courses)
            {
                var temp = new CourseViewModel(course);
                model.Add(temp);
            }
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

        [Authorize]
        public ActionResult AssignmentJson(int id)
        {
            var assignments = aService.GetAssignmentsInCourse(id);
            return Json(assignments, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrators, Teachers")]
        public ActionResult Teacher()
        {
            IdentityManager manager = new IdentityManager();
            var courses = cService.GetAllCourses();
            if (!User.IsInRole("Administrators"))
            {


                string UserId = User.Identity.GetUserId();

                if (!(manager.UserIsInRole(UserId, "Administrators")))
                {
                    string teacherName = AuthenticationManager.User.Identity.Name;

                    int Id = tService.GetTeacherIdByName(teacherName);

                    courses = aService.GetAllCoursesForTeacher(Id);
                }
            }

            List<CourseViewModel> model = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                var temp = new CourseViewModel(course);
                model.Add(temp);
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}