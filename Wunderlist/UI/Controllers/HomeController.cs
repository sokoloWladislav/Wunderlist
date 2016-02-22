using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "privacy-policy page";

            return View();
        }
    }
}