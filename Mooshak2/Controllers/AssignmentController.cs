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
        public ActionResult Instructions()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Teachers")]
        public ActionResult CreateNewAssignment(int teacherId)
        {
            TeacherService service = new TeacherService();
            Teacher t = service.GetTeacherById(teacherId);

            TeachersAssignment model = new TeachersAssignment(t);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Teachers")]
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

        [HttpPost]
        [Authorize(Roles = "Teachers")]
        public ActionResult Create(List<HttpPostedFileBase> file, FormCollection collection)
        {
            ///<summary>
            ///Get all the assignment data from the form
            /// </summary>
            var newAssignment = new Assignment();
            ///<summary>
            ///Convert date and time string to a DateTime variable
            /// </summary>
            newAssignment.Title = collection["assignmentName"];
            string datetimeStr = collection["date"].ToString() + " " + collection["time"].ToString();
            DateTime datetime = new DateTime();
            datetime = DateTime.ParseExact(datetimeStr, "yyyy-MM-dd HH:mm", null);
            newAssignment.DueDate = datetime;
            newAssignment.PublishDate = DateTime.Now;
            var pdf = file.ElementAt(0);
            string pdfName = pdf.FileName;
            var ID = Convert.ToInt32(collection["courseSelect"]);
            newAssignment.CourseID = ID;
            CourseService cService = new CourseService();
            var course  = cService.GetCourseByID(ID);
            string courseName = course.name;
            int mNumber = Convert.ToInt32(collection["noOfMilestones"]);
            int count = 1;
            ///<summary>
            ///Add all milestones to the assignment
            /// </summary>
            for (int i=1; i <= mNumber; i++)
            {
                Milestone milestone = new Milestone();
                milestone.Title = collection["mName" + i];
                milestone.Weight = Convert.ToInt32(collection["mWeight" + i]);
                newAssignment.Milestones.Add(milestone);
                ///<summary>
                ///Save the input file
                /// </summary>
                var input = file.ElementAt(count);
                string filename = input.FileName;
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Code/")
                                        + courseName + "//"
                                        + newAssignment.Title + "//"
                                        + milestone.Title);

                string inputPath = (Server.MapPath("~/Code/")
                                            + courseName
                                            + "//" + newAssignment.Title + "//"
                                            + milestone.Title + "//"
                                            + "input.txt");
                input.SaveAs(inputPath);
                count++;
                
                ///<summary>
                ///Save the output file
                /// </summary>
                var output = file.ElementAt(count);
                filename = output.FileName;
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Code/")
                                        + courseName + "//"
                                        + newAssignment.Title + "//"
                                        + milestone.Title);

                string outputPath = (Server.MapPath("~/Code/")
                                            + courseName
                                            + "//" + newAssignment.Title + "//"
                                            + milestone.Title + "//"
                                            + "output.txt");
                output.SaveAs(outputPath);
                count++;

            }
            ///<summary>
            ///Add a message to the user, letting him know that
            ///the operation as been successful
            /// </summary>
            ViewBag.Result = "Successfully created new assignment!";

            ///<summary>
            ///Save the description pdf file
            /// </summary>
            System.IO.Directory.CreateDirectory(Server.MapPath("~/Code/") 
                                        + courseName + "//"
                                        + newAssignment.Title);

            string path = (Server.MapPath("~/Code/") 
                                        + courseName
                                        + "//" + newAssignment.Title + "//"
                                        + newAssignment.Title
                                        +".pdf");
            pdf.SaveAs(path);

            ///<summary>
            ///Add the new assignment entry to the database
            /// </summary>
            context.Assignments.InsertOnSubmit(newAssignment);
            context.SubmitChanges();

            ///<summary>
            ///Create the model to return to the view
            /// </summary>
            string teachersName = AuthenticationManager.User.Identity.Name;  //commenta út til að leyfa fleiri en teacher

            TeacherService service = new TeacherService();
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
        [Authorize]
        public ActionResult Details(int id)
        {
            var model = _service.GetAssignmentByID(id);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Submit(HttpPostedFileBase file, FormCollection collection)
        {
            ///<summary>
            ///Get info of chosen assignment to navigate a path to save the file
            ///</summary>
            string mTitle = collection["milestoneSelect"];
            var milestone = context.Milestones.FirstOrDefault(x => x.Title == mTitle);
            var assignment = context.Assignments.SingleOrDefault(x => x.ID == milestone.AssignmentID);
            var course = context.Courses.SingleOrDefault(x => x.ID == assignment.CourseID);
            var submission = new DAL.Submission();
            submission.SubmitTime = DateTime.Now;
            string time = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            submission.MilestoneID = milestone.ID;
            submission.Title = User.Identity.Name;
            submission.UserName_ = User.Identity.Name;
            var username = submission.UserName_;
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
                                         + username + "\\"
                                         + time + "\\");

                path = Path.Combine(Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + username + "\\"
                                         + time + "\\", fileName);
                file.SaveAs(path);
            }


            ///<summary>
            ///If this is a zip file then we unzip it into the same folder.
            ///</summary>
            string extractPath = Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + username + "\\"
                                         + time + "\\";
            if (Path.GetExtension(path) == ".zip")
            {
                ZipFile.ExtractToDirectory(path, extractPath);
            }


            ///<summary>
            ///The base of this code comes from Dabs but I have modified it to
            ///suit our needs
            ///Commens from him are not in this summary format but in
            ///sing line comments marked by //
            ///</summary>
            var workingFolder = Server.MapPath("~/Code/")
                                         + course.Name + "\\"
                                         + assignment.Title + "\\"
                                         + milestone.Title + "\\"
                                         + username + "\\"
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
                ///<summary>
                ///Get the input from the input file
                /// </summary>
                var inputPath = Server.MapPath("~/Code/")
                                    + course.Name + "\\"
                                    + assignment.Title + "\\"
                                    + milestone.Title
                                    + "\\input.txt";
                List<string> input = System.IO.File.ReadAllLines(inputPath).ToList();
                List<string> lines = new List<string>();

                ///<summary>
                ///Foreach input test, we execute the program
                /// </summary>
                foreach (var inp in input)
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
                        ///We write the input to the command line
                        /// </summary>
                        processExe.StandardInput.WriteLine(inp);
                        ///<summary>
                        ///Get all the output from the program
                        /// </summary>
                        if (!processExe.WaitForExit(30000))
                        {
                            submission.Result = "Runtime error";
                            ///Save the new submission to the database
                            ///</summary>
                            context.Submissions.InsertOnSubmit(submission);
                            context.SubmitChanges();
                            ///<summary>
                            ///get model to return to the view
                            ///</summary>
                            var viewmodel = _service.GetAssignmentByID(assignment.ID);
                            return View("Details", viewmodel);
                        }

                        while (!processExe.StandardOutput.EndOfStream)
                        {
                            lines.Add(processExe.StandardOutput.ReadLine());
                        }
                    }
                    ///<summary>
                    ///Efter each output test, we add this ##END##
                    ///operator to indicate that each test has finished. This is
                    ///so it is easier to read this when we display
                    ///the results
                    /// </summary>
                    lines.Add("##END##");
                }
                System.IO.File.WriteAllLines(workingFolder + "userOutput.txt", lines);
                ///<summary>
                ///Here we get the output from the program.
                /// </summary>
                ///<summary>
                ///We get the expected output for this milestone and compare it 
                ///to the output from the user program.
                ///We add the result to the submission class
                /// </summary>
                /// 
                var outputPath = workingFolder + "userOutput.txt";
                List<string> outPut = new List<string>();
                List<List<string>> userResults = new List<List<string>>();
                foreach (var line in System.IO.File.ReadLines(outputPath))
                {

                    if (line == "##END##")
                    {
                        userResults.Add(outPut);
                        outPut = new List<string>();
                    }
                    else
                    {
                        outPut.Add(line);
                    }
                }
                var expectedPath = Server.MapPath("~/Code/")
                                     + course.Name + "\\"
                                     + assignment.Title + "\\"
                                     + milestone.Title
                                     + "\\output.txt";

                List<string> expectedOutput = new List<string>();
                List<List<string>> outputResults = new List<List<string>>();
                bool accepted = false;
                foreach (var line in System.IO.File.ReadLines(expectedPath))
                {

                    if (line == "##END##")
                    {
                        outputResults.Add(expectedOutput);
                        expectedOutput = new List<string>();
                    }
                    else
                    {
                        expectedOutput.Add(line);
                    }
                }
                for (int i = 0; i < outputResults.Count; i++)
                {
                    for (int j = 0; j < outputResults.ElementAt(i).Count; j++)
                    {
                        if (outputResults.ElementAt(i).ElementAt(j) != userResults.ElementAt(i).ElementAt(j))
                        {
                            accepted = false;
                            break;
                        }
                        else
                        {
                            accepted = true;
                        }
                    }
                    if (!accepted)
                    {
                        break;
                    }
                }
                if (accepted)
                {
                    submission.Result = "Accepted";
                }
                else if (!accepted)
                {
                    submission.Result = "Wrong answer";
                }

            }
            ///<summary>
            ///if there is no exe file then the compiler failed
            ///</summary>
            if (!(System.IO.File.Exists(exeFilePath)))
            {

                submission.Result = "Compile error";
                ///<summary>
                ///Write the compiler output error to a file so the
                ///teacher can see it
                /// </summary>
                System.IO.File.WriteAllText(workingFolder + "userOutput.txt", output);
            }
            ///Save the new submission to the database
            ///</summary>
            context.Submissions.InsertOnSubmit(submission);
            context.SubmitChanges();
            ///<summary>
            ///get model to return to the view
            ///</summary>
            var model = _service.GetAssignmentByID(assignment.ID);
            return View("Details",model);
        }
        [Authorize]
        public ActionResult Results(int id)
        {
            ///<summary>
            ///Get the submission the user wants to view
            /// </summary>
            SubmissionsService service = new SubmissionsService();
            var submission = service.GetSubmissionByID(id);

            ///<summary>
            ///Map the paths to the input, expected output and the user output
            /// </summary>
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

            var userPath = Server.MapPath("~/Code/")
                                     + submission.Course + "\\"
                                     + submission.Assignment + "\\"
                                     + submission.Milestone + "\\"
                                     + submission.Title + "\\"
                                     + submission.Time.Value.ToString("yyyy-MM-dd HH.mm.ss")
                                     + "\\userOutput.txt";


            ///<summary>
            ///Get the expected output for this milestone
            /// </summary>
            List<List<string>> output = new List<List<string>>();
                List<string> tempList = new List<string>();
                foreach (var line in System.IO.File.ReadLines(outputPath))
                {

                    if (line == "##END##")
                    {
                        output.Add(tempList);
                        tempList = new List<string>();
                    }
                    else
                    {
                        tempList.Add(line);
                    }
                }

                submission.Output = output;

                submission.Input = System.IO.File.ReadAllLines(inputPath).ToList();

                ///<summary>
                ///Get the user output for this milestone
                /// </summary>
                List<List<string>> userResults = new List<List<string>>();
                if (System.IO.File.Exists(userPath))
                {
                    List<string> temp = new List<string>();
                    foreach (var line in System.IO.File.ReadLines(userPath))
                    {

                        if (line == "##END##")
                        {
                            userResults.Add(temp);
                            temp = new List<string>();
                        }
                        else
                        {
                            temp.Add(line);
                        }
                    }
                submission.UserOutput = userResults;
                }
                else
                {
                    submission.UserOutput = new List<List<string>>();
                }

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
