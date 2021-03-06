﻿using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Mooshak2.Models.ViewModels
{

    public class AssignmentViewModel
    {
        private MooshakDataContext context = new MooshakDataContext();

        public string Title { get; set; }
        public int ID { get; set; }
        public string Course { get; set; }
        public DateTime? DueDate { get; set; }

        public List<AssignmentMilestoneViewModel> Milestones { get; set; }

        public AssignmentViewModel()
        {

        }

        public AssignmentViewModel(Assignment assignment)
        {
            this.ID = assignment.ID;
            this.Title = assignment.Title;
            this.Milestones = assignment.Milestones.Select(a => new AssignmentMilestoneViewModel(a)).ToList();
            Course course = context.Courses.SingleOrDefault(x => x.ID == assignment.CourseID);
            this.Course = course.Name;
            this.DueDate = assignment.DueDate;
        }
    }

    public class TeachersAssignment
    {
        public List<BasicCourseVM> courses { get; set; }
        public TeacherVM teacherInfo { get; set; }
        public TeachersAssignment()
        {

        }
        public TeachersAssignment(Teacher t)
        {
            courses = t.TeachersInCourses.Select(a => a.Course).Select(a => new BasicCourseVM(a)).ToList();
            teacherInfo = new TeacherVM(t);
        }
    }

    public class TeacherVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public TeacherVM(Teacher t)
        {
            this.id = t.Id;
            this.name = t.Name;
        }

    }
}