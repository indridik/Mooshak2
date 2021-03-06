﻿using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
    public class CourseViewModel
    {
        public string name { get; set; }
        public int ID { get; set; }
        public List<int> teachers { get; set; }
        public List<int> students { get; set; }
        public CourseViewModel()
        {

        }

        public CourseViewModel(Course course)
        {
            this.name = course.Name;
            this.ID = course.ID;
        }
    }

    public class BasicCourseVM
    {
        public string name { get; set; }
        public int Id { get; set; }
        public BasicCourseVM(Course course)
        {
            this.name = course.Name;
            this.Id = course.ID;
        }
    }
}