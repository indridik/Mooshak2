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
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Linq;

namespace Mooshak2.Controllers
{
    public class CourseController : Controller
    {
        private CourseService _service = new CourseService();

        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            var viewModel = _service.GetCourseByID(id);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            CourseService service = new CourseService();
            CreateCourseModel model = service.InitCreate();
            return View(model);
        }
        public ActionResult Edit()
        {
            CourseService service = new CourseService();
            List<Course> courses = service.GetAllCourses();
            EditCourseModel model = new EditCourseModel(courses);
            return View(model);
        }
        public ActionResult CreateCourse(CourseViewModel viewModel)
        {
            CourseService courseService = new CourseService();
            EntitySet<TeachersInCourse> teachers = new EntitySet<TeachersInCourse>();
            EntitySet<StudentsInCourse> students = new EntitySet<StudentsInCourse>();

            foreach (var teacher in viewModel.teachers)
            {
                teachers.Add(new TeachersInCourse()
                {
                    TeacherId = teacher
                });
            }

            foreach (var student in viewModel.students)
            {
                students.Add(new StudentsInCourse()
                {
                    StudentId = student
                });
            }

            Course model = new Course()
            {
                Name = viewModel.name,
                StudentsInCourses = students,
                TeachersInCourses = teachers
            };
            RequestResponse resp = courseService.CreateCourse(model);
            return View();
        }

        public JsonResult CreateCourseJson(CourseViewModel viewModel)
        {
            CourseService service = new CourseService();

            EntitySet<TeachersInCourse> teachers = new EntitySet<TeachersInCourse>();
            EntitySet<StudentsInCourse> students = new EntitySet<StudentsInCourse>();

            foreach (var teacher in viewModel.teachers)
            {
                teachers.Add(new TeachersInCourse()
                {
                    TeacherId = teacher
                });
            }

            foreach (var student in viewModel.students)
            {
                students.Add(new StudentsInCourse()
                {
                    StudentId = student
                });
            }

            Course model = new Course()
            {
                Name = viewModel.name,
                StudentsInCourses = students,
                TeachersInCourses = teachers
            };
            RequestResponse response = service.CreateCourse(model);
            ViewBag.result = "Course successfully created!";
            return Json(response);
        }
    }
}