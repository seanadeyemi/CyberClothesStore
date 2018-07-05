using ClassLibrary1;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Store.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly UserManager<User> userManager;

        public AccountController() : this(Startup.UserManagerFactory.Invoke())
        {

        }

        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }




        public ActionResult Login()
        {
            return View();

        }



        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    var identity = await userManager.CreateIdentityAsync(user, "ApplicationCookie");

                    identity.AddClaim(new Claim(ClaimTypes.Country, "Nigera"));
                    identity.AddClaim(new Claim(ClaimTypes.MobilePhone, "65555666555"));


                    Request.GetOwinContext().Authentication.SignIn(identity);


                    return Redirect(GetRedirectUrl(returnUrl));

                }

            }
           
                ModelState.AddModelError("", "Incorrect UserName or Password");
                return View(model);
            
        }

        public string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Admin");
            }


            return returnUrl;
        }



        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            var user = new User
            {
                 Email = model.UserName,
                 UserName = model.UserName
                 
               


            };



            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                var identity = await userManager.CreateIdentityAsync(user, "ApplicationCookie");
                //identity.AddClaim
                //Request.GetOwinContext().Authentication.SignIn(identity);
                Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(56), IsPersistent = true });

                return RedirectToAction("Index",  "Admin");
            }

            if (result.Errors.Any())
            {
               foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

            }


            return View(model);

        }






    }
}