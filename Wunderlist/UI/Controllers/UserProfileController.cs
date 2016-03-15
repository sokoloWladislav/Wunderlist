using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace UI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}