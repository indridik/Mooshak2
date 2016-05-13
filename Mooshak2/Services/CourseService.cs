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

        public RequestResponse DeleteCourse(int id)
        {
            try
            {
                Course c = context.Courses.FirstOrDefault(a => a.ID == id);
                if (c == null)
                    return new RequestResponse();

                context.TeachersInCourses.DeleteAllOnSubmit(c.TeachersInCourses);
                context.StudentsInCourses.DeleteAllOnSubmit(c.StudentsInCourses);
                context.Courses.DeleteOnSubmit(c);
                context.SubmitChanges();
                return new RequestResponse();
            }
            catch (Exception ex)
            {
                //TODO log
                return new RequestResponse(ex.Message, Status.Error);
            }
        }
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
        public Course GetCourseFromId(int courseId)
        {
            Course course = context.Courses.FirstOrDefault(a => a.ID == courseId);
            if(course == null)
            {
                return null;
            }
            return course;
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

        internal RequestResponse AddTeacherToCourse(int courseId, string teachersName)
        {
            try
            {
                TeacherService teacherService = new TeacherService();
                int teacherId = teacherService.GetTeacherIdByName(teachersName);
                if (teacherId == -1)
                    return new RequestResponse("Teacher does not exist", Status.Error);
                if (context.TeachersInCourses.Any(a => a.CourseId == courseId && a.TeacherId == teacherId))
                    return new RequestResponse("Teacher already in course", Status.Error);

                

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
        internal RequestResponse AddStudentToCourse(int courseId, string studentName)
        {
                try
                {
                StudentService service = new StudentService();
                int studentId = service.GetStudentIdByName(studentName);
                if (studentId == -1)
                    return new RequestResponse("Student does not exist", Status.Error);
                if (context.StudentsInCourses.Any(a => a.CourseId == courseId && a.StudentId == studentId))
                    return new RequestResponse("Student already in course", Status.Error);

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
        internal RequestResponse RemoveStudentFromCourse(int courseId, string studentName)
        {
            try
            {
                StudentService service = new StudentService();
                int studentId = service.GetStudentIdByName(studentName);
                StudentsInCourse student = context.StudentsInCourses.FirstOrDefault(a => a.CourseId == courseId && a.StudentId == studentId);
                if (student == null)
                    return new RequestResponse("Student not found", Status.Error);

                context.StudentsInCourses.DeleteOnSubmit(student);
                context.SubmitChanges();
                return new RequestResponse();
            }
            catch(Exception ex)
            {
                LogService.LogError("RemoveStudentFromCourse", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }

        }
        internal RequestResponse RemoveTeacherFromCourse(int courseId, string teacherName)
        {
            try
            {
                TeacherService teacherService = new TeacherService();
                int teacherId = teacherService.GetTeacherIdByName(teacherName);
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
        internal RequestResponse RemoveCourse(int courseId)
        {
            try
            {
                CourseService service = new CourseService();
                Course course = service.GetCourseFromId(courseId);
                if (course == null)
                    return new RequestResponse("Course not found", Status.Error);
                List<TeachersInCourse> teachers = context.TeachersInCourses.Where(a => a.CourseId == courseId).ToList();
                List<StudentsInCourse> students = context.StudentsInCourses.Where(a => a.CourseId == courseId).ToList();

                foreach (var teach in teachers)
                {
                    context.TeachersInCourses.DeleteOnSubmit(teach);
                }
                foreach (var stud in students)
                {
                    context.StudentsInCourses.DeleteOnSubmit(stud);
                }
                context.Courses.DeleteOnSubmit(course);
                context.SubmitChanges();
                return new RequestResponse();
            }
            catch(Exception ex)
            {
                LogService.LogError("RemoveCourse", ex);
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