using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Mooshak2.Services
{
    public class AssignmentsService
    {
        private ApplicationDbContext _db;

        public AssignmentsService()
        {
            _db = new ApplicationDbContext();
        }

        public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {
            List<AssignmentViewModel> viewModel = new List<AssignmentViewModel>();
            var assignments = _db.Assignments.Where(x => x.CourseID == courseID).ToList();
            if (assignments == null)
            {
                //TODO: kasta villu
            }
            foreach (var ass in assignments)
            {
                AssignmentViewModel temp = new AssignmentViewModel();
                temp.Title = ass.Title;
                temp.Milestones = _db.Milestones
                                  .Where(x => x.AssignmentID == ass.ID)
                                  .Select(x => new AssignmentMilestoneViewModel
                                  {
                                      Title = x.Title
                                  }).ToList();
                viewModel.Add(temp);
            }

            return viewModel;
        }

        public AssignmentViewModel GetAssignmentByID(int assignmentID)
        {
            var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
            if (assignment == null)
            {
                // TODO: kasta villu!
            }

            var milestones = _db.Milestones
                .Where(x => x.AssignmentID == assignmentID)
                .Select(x => new AssignmentMilestoneViewModel
                {
                    Title = x.Title
                })
                .ToList();

            var viewModel = new AssignmentViewModel
            {
                Title = assignment.Title,
                Milestones = milestones
            };

            return viewModel;
        }

    }
}