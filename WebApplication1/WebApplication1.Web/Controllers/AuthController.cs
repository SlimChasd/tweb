using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Application;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService = new AuthService(new ApplicationDbContext());

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.AuthenticateUser(username, password);
                if (user != null)
                {
                    SignInUser(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string password, string firstName, string lastName, string email, string phoneNumber)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.RegisterUser(username, password, firstName, lastName, email, phoneNumber);
                if (user != null)
                {
                    SignInUser(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Registration failed.");
            }
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(authCookie);
            return RedirectToAction("Login");
        }

        private void SignInUser(Customer user)
        {
            var authTicket = new FormsAuthenticationTicket(
                1, // version
                user.Username, // user name
                DateTime.Now, // creation
                DateTime.Now.AddMinutes(30), // expiration
                false, // persistent?
                user.Role // user data
            );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }
    }
}
