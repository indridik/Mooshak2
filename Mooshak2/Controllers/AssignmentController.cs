using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Services;
using System.Web.Mvc;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using Mooshak2.Models;

namespace Mooshak2.Controllers
{
    public class AssignmentController : Controller
    {
        private AssignmentsService _service = new AssignmentsService();

        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submit(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var model = _db.Assignments.Where(x => x.ID == id).SingleOrDefault();
            return View(model);
        }
       

        [HttpPost]
        public ActionResult Submit(HttpPostedFileBase file)
        {
            var path = "";
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName("file1.zip");
                Directory.CreateDirectory(@"C:\Temp\Mooshak2Code\" + User.Identity.Name + "\\");
                path = Path.Combine(@"C:\Temp\Mooshak2Code\" + User.Identity.Name + "\\", fileName);
                file.SaveAs(path);
            }

            string extractPath = "C:\\Temp\\Mooshak2Code\\" + User.Identity.Name + "\\";
            if (!System.IO.File.Exists(extractPath + "\\main.cpp"))
            {
                ZipFile.ExtractToDirectory(path, extractPath);
            }

            //var code = System.IO.File.ReadAllText(extractPath + "\\main.cpp");
            // To simplify matters, we declare the code here.
            // The code would of course come from the student!
            // Set up our working folder, and the file names/paths.
            // In this example, this is all hardcoded, but in a
            // real life scenario, there should probably be individual
            // folders for each user/assignment/milestone.
            var workingFolder = "C:\\Temp\\Mooshak2Code\\" + User.Identity.Name + "\\";
            var cppFileName = "main.cpp";
            var exeFilePath = workingFolder + "main.exe";

            // Write the code to a file, such that the compiler
            // can find it:
            //System.IO.File.WriteAllText(workingFolder + cppFileName, code);

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
                    // In this example, we don't try to pass any input
                    // to the program, but that is of course also
                    // necessary. We would do that here, using
                    // processExe.StandardInput.WriteLine(), similar
                    // to above.
                    // We then read the output of the program:
                    var lines = new List<string>();
                    while (!processExe.StandardOutput.EndOfStream)
                    {
                        lines.Add(processExe.StandardOutput.ReadLine());
                    }

                    ViewBag.Output = lines;
                }
            }
            else
            {
                var lines = output;
                ViewBag.Output = lines;
            }

            // TODO: We might want to clean up after the process, there
            // may be files we should delete etc.
            return View();
        }
    }
}