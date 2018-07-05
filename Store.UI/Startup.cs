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

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")

            });

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
    }
}
