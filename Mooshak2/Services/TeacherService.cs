using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
    public class TeacherService
    {
        private readonly MooshakDataContext context = new MooshakDataContext();
        public Teacher GetTeacherById(int id)
        {
            return context.Teachers.FirstOrDefault(a => a.Id == id);
        }

        public int GetTeacherIdByName(string name)
        {
            return context.Teachers.FirstOrDefault(a => a.Name == name).Id;
        }
    }
}