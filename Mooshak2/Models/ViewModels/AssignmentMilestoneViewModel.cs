using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.DAL;

namespace Mooshak2.Models.ViewModels
{
    public class AssignmentMilestoneViewModel
    {
        private MooshakDataContext context = new MooshakDataContext();
        public string Title { get; set; }
        public List<SubmissionViewModel> Submissions { get; set; }

        public AssignmentMilestoneViewModel()
        {

        }

        public AssignmentMilestoneViewModel(Milestone milestone)
        {
            this.Title = milestone.Title;
            this.Submissions = milestone.Submissions.Select(x => new SubmissionViewModel(x)).OrderByDescending(x => x.ID).ToList();
        }
    }
}