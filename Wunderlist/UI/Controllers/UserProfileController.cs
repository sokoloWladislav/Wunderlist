using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BLL.Interface.Interfaces;
using Microsoft.AspNet.Identity;

namespace UI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            ViewBag.UserProfileName = _userService.GetUserById(User.Identity.GetUserId()).UserProfileName;
            return View();
        }
    }
}