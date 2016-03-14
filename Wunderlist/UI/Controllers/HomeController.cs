using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
  
        [Authorize]
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