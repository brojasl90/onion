using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Enum;
using Web.Security;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado, (int)Roles.Vendedor)]
        public ActionResult Index()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];
            }
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado, (int)Roles.Vendedor)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado, (int)Roles.Vendedor)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}