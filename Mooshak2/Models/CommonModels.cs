using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
    public class CreateCourseModel
    {
        public List<Teacher> teachers { get; set; }
        public List<Student> students { get; set; }
        public CreateCourseModel(List<Teacher> t, List<Student> s)
        {
            this.teachers = t;
            this.students = s;
        }
    }
    public class EditCourseModel
    {
        public Teacher teachers { get; set; }
        public Student students { get; set; }
        public TeachersInCourse teachersInCourses { get; set; }
        public StudentsInCourse studentsInCourses { get; set; }
        public List<Course> courses { get; set; }
        public EditCourseModel(List<Course> c)
        {
            this.courses = c;
        }
    }
}