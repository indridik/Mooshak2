using Microsoft.AspNet.Identity;
using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
    public class SubmissionViewModel
    {
        private MooshakDataContext context = new MooshakDataContext();
        public SubmissionViewModel()
        {
            Input = new List<string>();
            Output = new List<string>();
            UserOutput = new List<string>();
        }
        public SubmissionViewModel(Submission submission)
        {
            this.Result = submission.Result;
            this.Time = submission.SubmitTime;
            Milestone milestone = context.Milestones.SingleOrDefault(x => x.ID == submission.MilestoneID);
            this.Milestone = milestone.Title;
            Assignment assignment = context.Assignments.SingleOrDefault(x => x.ID == milestone.AssignmentID);
            this.Assignment = assignment.Title;
            Course course = context.Courses.SingleOrDefault(x => x.ID == assignment.CourseID);
            this.Course = course.Name;
            this.ID = submission.ID;
            this.Title = submission.Title;
        }

        public string Title { get; set; }
        public List<string> Input { get; set; }
        public List<string> Output { get; set; }
        public List<string> UserOutput { get; set; }
        public string Assignment { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// Hluti verkefnis sem var skilað
        /// </summary>
        public string Milestone { get; set; }
        /// <summary>
        /// Niðurstaða skila
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Tímasetning skila
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// Hvaða kúrs er verið að skila verkefni í
        /// </summary>
        public string Course { get; set; }

    }
}