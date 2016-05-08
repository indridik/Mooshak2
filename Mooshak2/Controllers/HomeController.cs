﻿using Mooshak2.Models;
using Mooshak2.Models.Entities;
using System.Web.Mvc;
using Mooshak2.Models.ViewModels;
using System.Linq;

namespace Mooshak2.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var model = _db.Courses.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Mooshak 2.0";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hópmeðlimir.";

            return View();
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult Admin()
        {
            return View();
        }
    }
}