using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.DTO;
using BLL.Interface.Infrastructure;
using BLL.Interface.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserDTO userDto = new ApplicationUserDTO { UserName = model.Email, Password = model.Password };
                ClaimsIdentity claim = _userService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.IsPersistent
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserDTO userDto = new ApplicationUserDTO
                {
                    UserName = model.Email,
                    Password = model.Password,
                    UserProfileName = model.UserProfileName
                };
                OperationDetails operationDetails = _userService.CreateUser(userDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Login", "Account");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}