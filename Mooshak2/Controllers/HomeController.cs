﻿using Mooshak2.Models;
using Mooshak2.Models.Entities;
using System.Web.Mvc;
using Mooshak2.Models.ViewModels;
using System.Linq;
using Mooshak2.DAL;
using Mooshak2.Services;
using System.Collections.Generic;

namespace Mooshak2.Controllers
{
    public class HomeController : Controller
    {
        private MooshakDataContext context = new MooshakDataContext();
        private CourseService cService = new CourseService();
        private AssignmentsService aService = new AssignmentsService();
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

            var courses = cService.GetAllCourses();
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

        public ActionResult AssignmentJson(int id)
        {
            var assignments = aService.GetAssignmentsInCourse(id);
            return Json(assignments, JsonRequestBehavior.AllowGet);
        }
    }
}