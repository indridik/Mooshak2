using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
    public class StudentService
    {
        private readonly MooshakDataContext context = new MooshakDataContext();
        public int GetStudentIdByName(string name)
        {
            return context.Students.FirstOrDefault(a => a.UserName == name).ID;
        }
        public List<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }
    }
}