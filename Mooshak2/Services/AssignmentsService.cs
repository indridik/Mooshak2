﻿using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.DAL;

namespace Mooshak2.Services
{
    public class AssignmentsService
    {
        private MooshakDataContext context;

        public AssignmentsService()
        {
            context = new MooshakDataContext();
        }

        public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
        {
            try
            {
                List<Assignment> assignments = context.Assignments.Where(a => a.CourseID == courseID).ToList();

                if (!assignments.Any())
                    return new List<AssignmentViewModel>();

                List<AssignmentViewModel> viewModel = new List<AssignmentViewModel>();

                assignments.ForEach(a => viewModel.Add(new AssignmentViewModel(a)));

                return viewModel;
            }
            catch (Exception ex)
            {
                LogService.LogError("GetAssignmentsInCourse", ex);
                return new List<AssignmentViewModel>();
            }
        }

        internal RequestResponse CreateAssignment(Assignment model)
        {
            try
            {
                context.Assignments.InsertOnSubmit(model);
                context.SubmitChanges();

                return new RequestResponse();
            }
            catch (Exception ex)
            {
                LogService.LogError("CreateAssignment", ex);
                return new RequestResponse(ex.Message, Status.Error);
            }
        }

        public AssignmentViewModel GetAssignmentByID(int assignmentID)
        {
            Assignment assignment = context.Assignments.FirstOrDefault(a => a.ID == assignmentID);

            if (assignment == null)
                return null;

            return new AssignmentViewModel(assignment);

        }

        public List<Course> GetAllCoursesForTeacher(int teacherId)
        {
            try {
                
                Teacher teacher = context.Teachers.FirstOrDefault(a => a.Id == teacherId);
                return teacher.TeachersInCourses.Select(a => a.Course).ToList();
            }
            catch(Exception ex)
            {
                LogService.LogError("GetAllCoursesForTeacher", ex);
                return new List<Course>();
            }
        }

        public List<BasicCourseVM> GetAllCoursesVMForTeacher(int teacherId)
        {
            List<Course> courses = GetAllCoursesForTeacher(teacherId);
            return courses.Select(a => new BasicCourseVM(a)).ToList();
        }

        public void UpdateAssignment(string title, int id)
        {
            Assignment assToEdit = context.Assignments.FirstOrDefault(a => a.ID == id);
            assToEdit.Title = title;

            context.SubmitChanges();
        }
        

    }
}