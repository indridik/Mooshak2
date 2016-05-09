using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.DAL;

namespace Mooshak2.Services
{
    public class AssignmentsService
    {
        private ApplicationDbContext _db;
        private MooshakDataContext context;

        public AssignmentsService()
        {
            context = new MooshakDataContext();
            //_db = new ApplicationDbContext();
        }

        public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {
            try
            {
                List<Assignment> assignments = context.Assignments.Where(a => a.CourseID == courseID).ToList();

                if (!assignments.Any())
                    return new List<AssignmentViewModel>();

                List<AssignmentViewModel> viewModel = new List<AssignmentViewModel>();

                assignments.ForEach(a => viewModel.Add(new AssignmentViewModel(a)));

                return viewModel;
            }
            catch (Exception ex)
            {
                LogService.LogError("GetAssignmentsInCourse", ex);
                return new List<AssignmentViewModel>();
            }

            //List<AssignmentViewModel> viewModel = new List<AssignmentViewModel>();
            //var assignments = _db.Assignments.Where(x => x.Course == courseID).ToList();
            //if (assignments == null)
            //{
            //    //TODO: kasta villu
            //}
            //foreach (var ass in assignments)
            //{
            //    AssignmentViewModel temp = new AssignmentViewModel();
            //    temp.Title = ass.Title;
            //    temp.Milestones = _db.Milestones
            //                      .Where(x => x.AssignmentID == ass.ID)
            //                      .Select(x => new AssignmentMilestoneViewModel
            //                      {
            //                          Title = x.Title
            //                      }).ToList();
            //    viewModel.Add(temp);
            //}

            //return viewModel;
        }

        internal RequestResponse CreateAssignment(Assignment model)
        {
            try
            {
                context.Assignments.InsertOnSubmit(model);
                context.SubmitChanges();

                return new RequestResponse();
            }
            catch (Exception ex)
            {
                LogService.LogError("CreateAssignment", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }

        public AssignmentViewModel GetAssignmentByID(int assignmentID)
        {
            Assignment assignment = context.Assignments.FirstOrDefault(a => a.ID == assignmentID);

            if (assignment == null)
                return null;

            return new AssignmentViewModel(assignment);

            //var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
            //if (assignment == null)
            //{
            //    // TODO: kasta villu!
            //}

            //var milestones = _db.Milestones
            //    .Where(x => x.AssignmentID == assignmentID)
            //    .Select(x => new AssignmentMilestoneViewModel
            //    {
            //        Title = x.Title
            //    })
            //    .ToList();

            //var viewModel = new AssignmentViewModel
            //{
            //    Title = assignment.Title,
            //    Milestones = milestones
            //};

            //return viewModel;
        }
        public void UpdateAssignment(string title, int id)
        {
            Assignment assToEdit = context.Assignments.FirstOrDefault(a => a.ID == id);
            assToEdit.Title = title;

            context.SubmitChanges();
        }
        

    }
}