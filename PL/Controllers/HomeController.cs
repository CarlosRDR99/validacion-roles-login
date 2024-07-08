using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    [AuthRoles("SuperUsuario", "Empleado", "Cliente")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AuthRoles("SuperUsuario")]
        public ActionResult About()
        {
            return View();
        }

        [AuthRoles("Cliente", "Empleado")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}