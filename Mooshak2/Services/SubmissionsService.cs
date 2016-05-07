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

        public SubmissionViewModel GetStudentByID(int studentID)
        {
            return null;
        }

        public SubmissionViewModel GetMilestone(int assignmentID, string MilestoneTitle)
        {
            return null;
        }

        public SubmissionViewModel GetResult(string MilestoneResult)
        {
            return null;
        }
        public SubmissionViewModel GetLanguage(string AssignmentLanguage)
        {
            return null;
        }

        public SubmissionViewModel GetTime(DateTime subTime)
        {
            return null;
        }
        public SubmissionViewModel GetCourseByID(int CourseID)
        {
            return null;
        }

    }
}