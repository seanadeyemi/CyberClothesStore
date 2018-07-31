using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


using Owin;
using ClassLibrary1;

[assembly: OwinStartup(typeof(Store.UI.Startup))]

namespace Store.UI
{
    public class Startup
    {

        public static Func<UserManager<User>> UserManagerFactory { get; set; }

        public static Func<RoleManager<IdentityRole, string>> UserRoleFactory { get; private set; } = CreateRole;

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")

            });

            //app.CreatePerOwinContext<CustomUserManager>(CustomUserManager.Create);
            //app.CreatePerOwinContext<CustomSignInManager>(CustomSignInManager.Create);



            UserManagerFactory =() =>
                {
                    var usermanager = new UserManager<User>(new UserStore<User>(new ClothDbContext()));


                    usermanager.UserValidator = new UserValidator<User>(usermanager)
                    {
                        AllowOnlyAlphanumericUserNames = false
                    };

                    return usermanager;
                };

        }



        public static RoleManager<IdentityRole, string> CreateRole()
        {
            var dbContext = new ClothDbContext();
            var store = new RoleStore<IdentityRole, string, IdentityUserRole>(dbContext);
            var rolemanager = new RoleManager<IdentityRole, string>(store);

            return rolemanager;
        }


        //public class CustomUserManager : UserManager<User>
        //{
        //    public CustomUserManager(IUserStore<User> store)
        //        : base(store)
        //    {
        //    }
        //    public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        //    {
        //        var manager = new CustomUserManager(new UserStore<User>(context.Get<ClothDbContext>()));//(new CustomUserStore());
        //        return manager;
        //    }

        //}

        //public class CustomSignInManager : SignInManager<User, string>
        //{
        //    public CustomSignInManager(CustomUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        //    {

        //    }
        //    public static CustomSignInManager Create(IdentityFactoryOptions<CustomSignInManager> options, IOwinContext context)
        //        {
        //            return new CustomSignInManager(context.GetUserManager<CustomUserManager>(), context.Authentication);
        //        }
        //}

    }
}
