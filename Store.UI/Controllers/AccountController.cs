using ClassLibrary1;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private readonly RoleManager<IdentityRole, string> roleManager;

        public AccountController() : this(Startup.UserManagerFactory.Invoke(), Startup.UserRoleFactory.Invoke())
        {

        }

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole, string> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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

                //var user = await userManager.FindByNameAsync(model.UserName);




                //userManager.IsEmailConfirmed

                if (user != null)
                {
                    //if(!await userManager.IsEmailConfirmedAsync(user.Id))
                    //{
                    //    ViewBag.errorMessage = "You must have a confirmed email to log in.";
                    //    return View("Error");
                    //}

                    var signInManager = Request.GetOwinContext().Authentication;

                    // var result = await signInManager.PasswordSignInAsync(model.UserName, model.)


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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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

            string role = "User";

            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                if (!roleManager.RoleExists(role))
                {
                    var identityRole = new IdentityRole { Name = role };
                    roleManager.Create<IdentityRole, string>(identityRole);
                }
                else
                {
                    result = await userManager.AddToRoleAsync(user.Id, role);
                }





                // string code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);

                // var callbackUrl = Url.Action("ConfirmEmail", "Account",
                //new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                // await userManager.SendEmailAsync(user.Id,"Confirm your account",
                //     "Please confirm your account by clicking <a href=\""  + callbackUrl + "\">here</a>");

                var identity = await userManager.CreateIdentityAsync(user, "ApplicationCookie");
                //identity.AddClaim

                //  Comment the following line to prevent log in until the user is confirmed.
                //  await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                Request.GetOwinContext().Authentication.SignIn(identity);
                // Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(56), IsPersistent = true });




                // Uncomment to debug locally 
                // TempData["ViewBagLink"] = callbackUrl;

                //ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                //                + "before you can log in.";

                //return View("Info");


                //return RedirectToAction("Index",  "Admin");
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


        [HttpGet]
        public ActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditUser(string userId)
        {
            var user = userManager.FindById(userId);

            var roles = roleManager.Roles.ToList();

            var userRoles = userManager.GetRoles(user.Id);



            if (userRoles.Any())
            {
                var currentRole = userRoles.FirstOrDefault();
                if (currentRole != null)
                {
                    var role = roleManager.FindByName(currentRole);
                    if (role != null)
                    {
                        ViewBag.Roles = new SelectList(roles, "Id", "Name", role.Id);
                    }

                }

            }
            else
            {
                ViewBag.Roles = new SelectList(roles, "Id", "Name");
            }




            UserViewModel uvm = (UserViewModel)user;

            return View(uvm);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(uvm.Id);
                if (user != null)
                {

                    user.UserName = uvm.UserName;
                    user.PhoneNumber = uvm.PhoneNumber;
                    user.Email = uvm.Email;
                    var role = roleManager.FindById(uvm.Role);
                    userManager.AddToRole(user.Id, role.Name);

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"{user.UserName} was successfully updated.";
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                        return View(uvm);
                    }



                }

                return View(uvm);
            }
            else
            {
                ModelState.AddModelError("", "One or more required fields are not valid");
                return View(uvm);
            }

        }

        [HttpGet]
        public ActionResult RoleList()
        {

            var roles = roleManager.Roles.ToList();
            var rlvm = new RoleListViewModel { Roles = roles };
            return View(rlvm);
        }
        [HttpGet]
        public ActionResult EditRole(string Id)
        {
            var role = roleManager.FindById(Id);


            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(IdentityRole rol)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(rol.Id);
                if (role != null)
                {

                    role.Name = rol.Name;

                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"{role.Name} was successfully updated.";
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                        return View(rol);
                    }



                }

                return View(rol);
            }
            else
            {
                ModelState.AddModelError("", "One or more required fields are not valid");
                return View(rol);
            }

        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(IdentityRole cvm)
        {
            if (ModelState.IsValid)
            {
                var existingrole = await roleManager.FindByNameAsync(cvm.Name);

                if (existingrole != null)
                {
                    ModelState.AddModelError("", "A role with that name already exists");
                    return View(cvm);
                }

                var role = new IdentityRole
                {
                    Name = cvm.Name


                };
                roleManager.Create<IdentityRole, string>(role);

                TempData["message"] = string.Format("{0} has been created.", cvm.Name);

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                //something went wrong with the form data values
                return View(cvm);
            }
        }

        public ActionResult DeleteRole(string Id)
        {
            var role = roleManager.FindById(Id);

            if (role != null)
            {
                var result = roleManager.Delete(role);

                if (result.Succeeded)
                {
                    TempData["message"] = $"{role.Name} was successfully deleted.";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddUser()
        {

            return View(new UserViewModel());
        }
    }
}