using Mooshak2.Models;
using System.Web.Mvc;

namespace Mooshak2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IdentityManager manager = new IdentityManager();
            if(!manager.RoleExists("Administrators"))
            {
                manager.CreateRole("Administrators");
            }
            if(!manager.RoleExists("Teachers"))
            {
                manager.CreateRole("Teachers");
            }
            return View();
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
    }
}