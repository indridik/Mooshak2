using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Services;
using System.Web.Mvc;
using Mooshak2.Models.Entities;
using Mooshak2.Models.ViewModels;
using Mooshak2.DAL;
using Mooshak2.Models;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using Microsoft.Owin.Security;

namespace Mooshak2.Controllers
{
    public class AssignmentController : Controller
    {
        private MooshakDataContext context = new MooshakDataContext();
        private AssignmentsService _service = new AssignmentsService();

        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateNewAssignment(int teacherId)
        {
            TeacherService service = new TeacherService();
            Teacher t = service.GetTeacherById(teacherId);

            TeachersAssignment model = new TeachersAssignment(t);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            //TODO only allow authenticated teachers to create assignment
            string teachersName = AuthenticationManager.User.Identity.Name;  //commenta út til að leyfa fleiri en teacher

            TeacherService service = new TeacherService();

            //int id = 1; //hardcoded dabs
            int id = service.GetTeacherIdByName(teachersName); //commenta út ef nota á hardcoded dabs
            
            Teacher t = service.GetTeacherById(id);

            TeachersAssignment model = new TeachersAssignment(t);
            return View(model);
        }

        public JsonResult Create(Assignment model)
        {
            AssignmentsService service = new AssignmentsService();
            RequestResponse response = service.CreateAssignment(model);
            return Json(response);
        }

        public ActionResult Details(int id)
        {
            var model = _service.GetAssignmentByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Submit(HttpPostedFileBase file, FormCollection collection)
        {
            ///<summary>
            ///Get info of chosen assignment to navigate a path to save the file
            ///</summary>
            string mTitle = collection["milestoneSelect"];
            var milestone = context.Milestones.SingleOrDefault(x => x.Title == mTitle);
            var assignment = context.Assignments.SingleOrDefault(x => x.ID == milestone.AssignmentID);
            var course = context.Courses.SingleOrDefault(x => x.ID == assignment.CourseID);
            var submission = new DAL.Submission();
            submission.SubmitTime = DateTime.Now;
            string time = DateTime.Now.ToString("yyyyMMddTHHmmss");
            submission.MilestoneID = milestone.ID;
            submission.Title = User.Identity.Name;
            var path = "";
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                ///<summary>
                ///Create a folder from Assignment/Milestone/User/noOfSubmissions
                ///and save the file there
                ///</summary>
                Directory.CreateDirectory(Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + User.Identity.Name + "\\"
                                         + time + "\\");

                path = Path.Combine(Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + User.Identity.Name + "\\"
                                         + time + "\\", fileName);
                file.SaveAs(path);
            }


            ///<summary>
            ///If this is a zip file then we unzip it into the same folder. A user
            ///will always upload a main.cpp file so if it exists then this is not
            ///a zip file.
            ///</summary>
            string extractPath = Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + User.Identity.Name + "\\"
                                         + time + "\\";
            if (Path.GetExtension(extractPath) == ".zip")
            {
                ZipFile.ExtractToDirectory(path, extractPath);
            }


            ///<summary>
            ///The base of this code comes from Dabs but I have modified it to
            ///suit our needs
            ///</summary>
            var workingFolder = Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + User.Identity.Name + "\\"
                                         + time + "\\";
            var cppFileName = "main.cpp";
            var exeFilePath = workingFolder + "main.exe";


            // In this case, we use the C++ compiler (cl.exe) which ships
            // with Visual Studio. It is located in this folder:
            var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";
            // There is a bit more to executing the compiler than
            // just calling cl.exe. In order for it to be able to know
            // where to find #include-d files (such as <iostream>),
            // we need to add certain folders to the PATH.
            // There is a .bat file which does that, and it is
            // located in the same folder as cl.exe, so we need to execute
            // that .bat file first.

            // Using this approach means that:
            // * the computer running our web application must have
            //   Visual Studio installed. This is an assumption we can
            //   make in this project.
            // * Hardcoding the path to the compiler is not an optimal
            //   solution. A better approach is to store the path in
            //   web.config, and access that value using ConfigurationManager.AppSettings.

            // Execute the compiler:
            Process compiler = new Process();
            compiler.StartInfo.FileName = "cmd.exe";
            compiler.StartInfo.WorkingDirectory = workingFolder;
            compiler.StartInfo.RedirectStandardInput = true;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.UseShellExecute = false;

            compiler.Start();
            compiler.StandardInput.WriteLine("\"" + compilerFolder + "vcvars32.bat" + "\"");
            compiler.StandardInput.WriteLine("cl.exe /nologo /EHsc " + cppFileName);
            compiler.StandardInput.WriteLine("exit");
            string output = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            compiler.Close();

            // Check if the compile succeeded, and if it did,
            // we try to execute the code:
            if (System.IO.File.Exists(exeFilePath))
            {
                var processInfoExe = new ProcessStartInfo(exeFilePath, "");
                processInfoExe.UseShellExecute = false;
                processInfoExe.RedirectStandardInput = true;
                processInfoExe.RedirectStandardOutput = true;
                processInfoExe.RedirectStandardError = true;
                processInfoExe.CreateNoWindow = true;
                using (var processExe = new Process())
                {
                    processExe.StartInfo = processInfoExe;
                    processExe.Start();
                   
                    ///<summary>
                    ///Here we get the input for this miletone which is saved
                    ///in a folder like /Assignment/Milestone
                    ///</summary>
                    var inputPath = Server.MapPath("~/Code/") 
                                    + course.Name + "\\"
                                    + assignment.Title + "\\" 
                                    + milestone.Title 
                                    + "\\input.txt";
                    var input = System.IO.File.ReadAllText(inputPath);
                    ///<summary>
                    ///We write the input to the command line
                    /// </summary>
                    processExe.StandardInput.WriteLine(input);
                    string lines = "";

                    ///<summary>
                    ///Here we get the output from the program.
                    /// </summary>
                    while (!processExe.StandardOutput.EndOfStream)
                    {
                        lines += processExe.StandardOutput.ReadLine();
                    }
                    System.IO.File.WriteAllText(workingFolder + "userInput.txt", lines);

                    ///<summary>
                    ///We get the expected output for this milestone and compare it 
                    ///to the output from the user program.
                    ///We add the result to the submission class
                    /// </summary>
                    /// 
                    

                    var outputPath = Server.MapPath("~/Code/")
                                     + course.Name + "\\"
                                     + assignment.Title + "\\" 
                                     + milestone.Title 
                                     + "\\output.txt";

                    var expectedOutput = System.IO.File.ReadAllText(outputPath);
                    if(lines == expectedOutput)
                    {
                        submission.Result = "Accepted";
                    }
                    else
                    {
                        submission.Result = "Wrong answer";
                    }
                }
            }

            ///<summary>
            ///If we get here, there is no .exe file so the compiler has failed
            ///</summary>
            else
            {
                submission.Result = "Compile error";
            }

            ///<summary>
            ///Save the new submission to the database
            ///</summary>
            context.Submissions.InsertOnSubmit(submission);
            context.SubmitChanges();

            ///<summary>
            ///
            ///</summary>
            var model = _service.GetAssignmentByID(assignment.ID);
                return View("Details",model);
        }

        public ActionResult Results(int id)
        {
            SubmissionsService service = new SubmissionsService();
            var submission = service.GetSubmissionByID(id);

            var outputPath = Server.MapPath("~/Code/")
                             + submission.Course + "\\"
                             + submission.Assignment + "\\"
                             + submission.Milestone
                             + "\\output.txt";

            var inputPath = Server.MapPath("~/Code/")
                                     + submission.Course + "\\"
                                     + submission.Assignment + "\\"
                                     + submission.Milestone
                                     + "\\input.txt";

            var expectedPath = Server.MapPath("~/Code/")
                                     + submission.Course + "\\"
                                     + submission.Assignment + "\\"
                                     + submission.Milestone + "\\"
                                     + User.Identity.Name + "\\"
                                     + submission.Time.Value.ToString("yyyyMMddTHHmmss")
                                     + "\\userInput.txt";

            submission.Output = System.IO.File.ReadAllText(outputPath);

            submission.Input = System.IO.File.ReadAllText(inputPath);

            submission.UserOutput = System.IO.File.ReadAllText(expectedPath);

            return View(submission);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }


}
