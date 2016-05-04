using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mooshak2.Services;

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

        public ActionResult Details(int id)
        {
            var viewModel = _service.GetAssignmentByID(id);

            return View(viewModel);
        }
    }
}