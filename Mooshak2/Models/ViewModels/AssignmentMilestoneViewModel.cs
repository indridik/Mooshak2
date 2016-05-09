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
        public string Title { get; set; }

        public AssignmentMilestoneViewModel()
        {

        }

        public AssignmentMilestoneViewModel(Milestone milestone)
        {
            this.Title = milestone.Title;
        }
    }
}