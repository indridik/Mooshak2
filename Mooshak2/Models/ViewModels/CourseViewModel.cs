using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
    public class CourseViewModel
    {
        /// <summary>
        /// ID kúrs
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Nafn kúrs
        /// </summary>
        public string Name { get; set; }

        public CourseViewModel()
        {

        }

        public CourseViewModel(Course course)
        {
            this.ID = course.ID;
            this.Name = course.Name;
        }
    }
}