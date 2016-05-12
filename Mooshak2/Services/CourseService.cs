using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.Models.Entities;
using Mooshak2.DAL;

namespace Mooshak2.Services
{
    public class CourseService
    {
        public CreateCourseModel InitCreate()
        {
            List<Teacher> teachers = context.Teachers.ToList();
            List<Student> students = context.Students.OrderBy(a => a.UserName).ToList();
            return new CreateCourseModel(teachers, students);
        }
        //private ApplicationDbContext _db;
        private readonly MooshakDataContext context;

        public CourseService()
        {
            this.context = new MooshakDataContext();
        }
        public CourseViewModel GetCourseByID(int courseID)
        {
            Course course = context.Courses.FirstOrDefault(a => a.ID == courseID);

            if(course == null)
            {
                //TODO: Throw einen error

                //return null;
                return new CourseViewModel();
            }

            return new CourseViewModel(course);
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = context.Courses.ToList();
            return courses;
        }
        internal RequestResponse CreateCourse(Course model)
        {
            try
            {
                context.Courses.InsertOnSubmit(model);
                context.SubmitChanges();

                return new RequestResponse();
            }
            catch (Exception ex)
            {
                LogService.LogError("CreateCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }

        internal RequestResponse AddTeacherToCourse(int courseId, int teacherId)
        {
            try
            {
                TeachersInCourse newteacher = new TeachersInCourse()
                {
                    CourseId = courseId,
                    TeacherId = teacherId
                };
                context.TeachersInCourses.InsertOnSubmit(newteacher);
                context.SubmitChanges();
                return new RequestResponse();
            }
            catch (Exception ex)
            {
                LogService.LogError("AddTeacherToCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }
        internal RequestResponse AddStudentToCourse(int courseId, int studentId)
        {
            try {
                StudentsInCourse newStudent = new StudentsInCourse()
                {

                    CourseId = courseId,
                    StudentId = studentId
                };
                context.StudentsInCourses.InsertOnSubmit(newStudent);
                context.SubmitChanges();
                return new RequestResponse();
            }
            catch(Exception ex)
            {
                LogService.LogError("AddStudentToCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }
        internal RequestResponse RemoveStudentFromtCourse(int courseId, int studentId)
        {
            try
            {
                StudentsInCourse student = context.StudentsInCourses.FirstOrDefault(a => a.CourseId == courseId && a.StudentId == studentId);
                if (student == null)
                    return new RequestResponse("Student not found", Status.Error);

                context.StudentsInCourses.DeleteOnSubmit(student);
                return new RequestResponse();
            }
            catch(Exception ex)
            {
                LogService.LogError("RemoveStudentFromCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }

        }
        internal RequestResponse RemoveTeacherFromCourse(int courseId, int teacherId)
        {
            try
            {
                TeachersInCourse teacher = context.TeachersInCourses.FirstOrDefault(a => a.CourseId == courseId && a.TeacherId == teacherId);
                if (teacher == null)
                    return new RequestResponse("Teacher not found", Status.Error);

                context.TeachersInCourses.DeleteOnSubmit(teacher);
                context.SubmitChanges();
                return new RequestResponse();
            }
            catch (Exception ex)
            {
                LogService.LogError("RemoveTeacherFromCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }

        public void UpdateCourse(string name, int id)
        {
            Course courseToEdit = context.Courses.FirstOrDefault(a => a.ID == id);
            courseToEdit.Name = name;

            context.SubmitChanges();
        }

        public List<Course> GetAllCoursesForStudent(int studentId)
        {
            try
            {

                Student student = context.Students.FirstOrDefault(a => a.ID == studentId);
                return student.StudentsInCourses.Select(a => a.Course).ToList();
            }
            catch (Exception ex)
            {
                LogService.LogError("GetAllCoursesForStudent", ex);
                return new List<Course>();
            }
        }
    }
}