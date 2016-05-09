using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.Models.Entities;
using Mooshak2.DAL;

namespace Mooshak2.Services
{
    public class CourseService
    {
        //private ApplicationDbContext _db;
        private readonly MooshakDataContext context;

        public CourseService()
        {
            this.context = new MooshakDataContext();
        }
        public CourseViewModel GetCourseByID(int courseID)
        {
            Course course = context.Courses.FirstOrDefault(a => a.ID == courseID);

            if(course == null)
            {
                //TODO: Throw einen error

                //return null;
                return new CourseViewModel();
            }

            return new CourseViewModel(course);
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = context.Courses.ToList();
            return courses;
        }
        internal RequestResponse CreateCourse(Course model)
        {
            try
            {
                context.Courses.InsertOnSubmit(model);
                context.SubmitChanges();

                return new RequestResponse();
            }
            catch (Exception ex)
            {
                LogService.LogError("CreateCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }
        public void UpdateCourse(string name, int id)
        {
            Course courseToEdit = context.Courses.FirstOrDefault(a => a.ID == id);
            courseToEdit.Name = name;

            context.SubmitChanges();
        }
    }
}