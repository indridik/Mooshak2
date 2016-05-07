using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Mooshak2.Services
{
    public class UserServices
    {
        private ApplicationDbContext _db;

        public UserServices()
        {
            _db = new ApplicationDbContext();
        }
        public UserViewModel GetUserByID(int userID)
        {
            var user = _db.Users.Where(x => x.UserID == userID).SingleOrDefault();
            if (user == null)
            {
                //TODO: henda error
                return null;
            }
            UserViewModel viewModel = new UserViewModel
            {
                ID = user.UserID,
                UserName = user.UserName,
            };

            return viewModel;
        }

        public List<StudentViewModel> GetAllStudents()
        {
            var result = _db.CourseStudents.ToList();
            List<StudentViewModel> students = new List<StudentViewModel>();
            foreach(var student in result)
            {
                StudentViewModel newStudent = new StudentViewModel();
                newStudent.UserName = student.UserName;
                students.Add(newStudent);
            }

            return students;
        }

        

    }
}