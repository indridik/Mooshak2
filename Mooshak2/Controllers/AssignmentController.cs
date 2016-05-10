using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Services;
using System.Web.Mvc;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using Mooshak2.DAL;
using Mooshak2.Models;
using Microsoft.AspNet.Identity;

namespace Mooshak2.Controllers
{
    public class AssignmentController : Controller
    {
        private AssignmentsService _service = new AssignmentsService();

        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var viewModel = _service.GetAssignmentByID(id);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            CourseService courseService = new CourseService();
            AssignmentsService assignmentService = new AssignmentsService();
            Assignment assignment = new Assignment();
            List<Course> courses = courseService.GetAllCourses();
            CreateAssignment model = new CreateAssignment(courses);
            List<CourseViewModel> coursesForTeacher = assignmentService.GetAllCoursesForTeacher(User.Identity.Name);

            return View(model);   
        }

        public JsonResult CreateJson(Assignment model)
        {
            AssignmentsService service = new AssignmentsService();
            RequestResponse response = service.CreateAssignment(model);
            return Json(response);
        }
    }
}