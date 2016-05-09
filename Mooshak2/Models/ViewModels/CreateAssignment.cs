using Mooshak2.DAL;
using Mooshak2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
    public class CreateAssignment : Assignment
    {
        public List<Course> courses { get; set; }
        public CreateAssignment() : base()
        {

        }

        public CreateAssignment(List<Course> courses) : base()
        {
            this.courses = courses;
        }
    }
}