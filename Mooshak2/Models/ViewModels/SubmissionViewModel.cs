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
        public SubmissionViewModel(Submission submission)
        {
            this.Result = submission.Result;
            this.Time = submission.SubmitTime;
            Milestone milestone = context.Milestones.SingleOrDefault(x => x.ID == submission.MilestoneID);
            this.Milestone = milestone.Title;
            Assignment assignment = context.Assignments.SingleOrDefault(x => x.ID == milestone.AssignmentID);
            Course course = context.Courses.SingleOrDefault(x => x.ID == assignment.ID);
            this.Course = course.Name;

        }
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