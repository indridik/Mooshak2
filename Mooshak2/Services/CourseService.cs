using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Mooshak2.Services
{
    public class CourseService
    {
        private ApplicationDbContext _db;

        public CourseService()
        {
            _db = new ApplicationDbContext();
        }
        public CourseViewModel GetCourseByID(int courseID)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == courseID);
            if(course == null)
            {
                ///TODO: Throw einen error
                return null;
            }
            var viewModel = new CourseViewModel
            {
                ID = course.ID,
                Name = course.Name
            };
            return viewModel;
        }
    }
}