using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.DAL;
using Microsoft.AspNet.Identity;

namespace Mooshak2.Models.ViewModels
{
    public class AssignmentMilestoneViewModel
    {
        private MooshakDataContext context = new MooshakDataContext();
        public string Title { get; set; }
        public List<SubmissionViewModel> Submissions { get; set; }

        public AssignmentMilestoneViewModel()
        {
            this.Submissions = new List<SubmissionViewModel>();
        }

        public AssignmentMilestoneViewModel(Milestone milestone)
        {
            this.Title = milestone.Title;
            this.Submissions = new List<SubmissionViewModel>();
            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            var submissions = milestone.Submissions.Where(x  => x.MilestoneID == milestone.ID).OrderByDescending(x => x.ID).OrderBy(x => x.Result).ToList();
            if(!(HttpContext.Current.User.IsInRole("Teachers") || HttpContext.Current.User.IsInRole("Administrators")))
            {
                submissions = submissions.Where(x => x.UserName_ == userName).ToList();

            }
            foreach (var sub in submissions)
            {
                this.Submissions.Add(new SubmissionViewModel(sub));
            } 
        }
    }
}