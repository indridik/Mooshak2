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
            try
            {
                return context.Teachers.FirstOrDefault(a => a.Name.ToLower() == name.ToLower()).Id;
            }
            catch (Exception ex)
            {
                //TODO LOG
                return -1;
            }
        }
        public List<Teacher> GetAllTeachers()
        {
            return context.Teachers.ToList();
        }
    }
}