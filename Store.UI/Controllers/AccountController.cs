using ClassLibrary1;
using Facebook;
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
                //Request.GetOwinContext().Authentication.GetExternalLoginInfo()
                //Request.GetOwinContext().Authentication.

                //new UserLoginInfo();
               // userManager.FindAsync(new UserLoginInfo(loginProvider:, providerKey:));



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
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = "Admin/Index" }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = Request.GetOwinContext().Authentication.GetExternalLoginInfo();
            ExternalRegisterViewModel exrvm = new ExternalRegisterViewModel();

            if(loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            UserLoginInfo lg = loginInfo == null ? null : new UserLoginInfo(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey);
            if(lg != null)
            {
                var user = await userManager.FindAsync(lg);
               


                if(user == null)
                {
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    if(loginInfo.Login.LoginProvider == "Google")
                    {
                        var Email = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                        var FirstName = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
                        var LastName = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value;
                        exrvm.UserName = Email;
                       
                        exrvm.FirstName = FirstName;
                        exrvm.LastName = LastName;
                        exrvm.Picture = string.Empty;
                    }
                    else if (loginInfo.Login.LoginProvider == "Facebook")
                    {
                        var identity = Request.GetOwinContext().Authentication.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                        var AccessToken = identity.FindFirstValue("FacebookAccessToken");
                        var FbClient = new FacebookClient(AccessToken);
                        dynamic Email = FbClient.Get("/me?fields=email");
                        dynamic FirstName = FbClient.Get("/me?fields=first_name");
                        dynamic LastName = FbClient.Get("/me?fields=last_name");
                        dynamic Picture = FbClient.Get("/me?fields=picture");

                        string picture = Picture.picture.data.url;
                        string firstname = FirstName.first_name;
                        string lastname = LastName.last_name;
                        exrvm.UserName = Email.email;
                        exrvm.FirstName = firstname;
                        exrvm.LastName = lastname;
                        exrvm.Picture = picture;



                    }





                }
                else
                {
                    var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalCookie);
                    Request.GetOwinContext().Authentication.SignIn(identity);
                }


            }
            //RedirectToAction("Index","Admin");
            return View("ExternalRegister", exrvm);
        }


        [HttpPost]
        
        public async Task<ActionResult> ExternalLoginRegister(ExternalRegisterViewModel evm)
        {

            var loginInfo = Request.GetOwinContext().Authentication.GetExternalLoginInfo();


            if (ModelState.IsValid)
            {
                if(loginInfo == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new User
                {
                    Email = evm.UserName,
                    UserName = evm.UserName
                };

                string role = "User";
                var result = await userManager.CreateAsync(user);

                if(result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user.Id, loginInfo.Login);
                    if(result.Succeeded)
                    {
                        var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalCookie);

                        Request.GetOwinContext().Authentication.SignIn(identity);

                        return RedirectToAction("Index", "Admin");
                    }
                }
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }

                }


            }



            return View("ExternalRegister", evm);
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


        internal class ChallengeResult : HttpUnauthorizedResult
        {

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }



            public ChallengeResult(string provider, string redirectUri):this(provider,redirectUri,  null)
            {

            }
            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                UserId = userId;
                RedirectUri = redirectUri;
            }


            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri  };
                if(UserId != null)
                {
                    properties.Dictionary["XsrfId"] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);

            }
        }
    }
}