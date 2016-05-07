using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
    public class SubmissionsService
    {
        private ApplicationDbContext _db;
        public SubmissionsService()
        {
            _db = new ApplicationDbContext();
        }

        public StudentViewModel GetStudentByID(int studentID)
        {
            var student = _db.Users.SingleOrDefault(x => x.ID == studentID);
            if(student == null)
            {
                return null;
            }
            var viewModel = new StudentViewModel
            {
                FullName = student.FullName,
                UserName = student.UserName
            };
            return viewModel;
        }

        public SubmissionViewModel GetMilestone(int assignmentID, string milestoneTitle)
        {
            var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
            if(assignment == null)
            {
                return null;
            }

            return null;
        }

        public SubmissionViewModel GetResult(string milestoneResult)
        {
            return null;
        }
        public SubmissionViewModel GetLanguage(string sssignmentLanguage)
        {
            return null;
        }

        public SubmissionViewModel GetTime(DateTime subTime)
        {
            return null;
        }
        public CourseViewModel GetCourseByID(int courseID)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == courseID);
            if(course == null)
            {
                //TODO: Throwa error
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