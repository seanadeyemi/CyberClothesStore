using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


using Owin;
using ClassLibrary1;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;

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

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.CreatePerOwinContext<CustomUserManager>(CustomUserManager.Create);
            //app.CreatePerOwinContext<CustomSignInManager>(CustomSignInManager.Create);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "120852303083 - ma5e842k7ss18jd0aea3ilroltdq5heh.apps.googleusercontent.com",
            //    ClientSecret = "WkiiXsQipBYXc7A0zSa4OCG7"
            //});

            var facebookAuthenticationOptions = new FacebookAuthenticationOptions();
            facebookAuthenticationOptions.Scope.Add("public_profile");
            facebookAuthenticationOptions.Scope.Add("email");
            facebookAuthenticationOptions.AppId = "219310835433760";
            facebookAuthenticationOptions.AppSecret = "8bed1526ef97cf57d9eceb9bea702a69";
            facebookAuthenticationOptions.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = context =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FaceBookAccessToken",
                        context.AccessToken));
                    return Task.FromResult(true);
                }
            };

            facebookAuthenticationOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(facebookAuthenticationOptions);



            var googleAuthenticationOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "120852303083-ma5e842k7ss18jd0aea3ilroltdq5heh.apps.googleusercontent.com",
                ClientSecret = "WkiiXsQipBYXc7A0zSa4OCG7",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("GoogleAccessToken", context.AccessToken));
                        foreach (var claim in context.User)
                        {
                            var claimType = string.Format("urn:google:{0}", claim.Key);
                            string ClaimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, ClaimValue))
                                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, ClaimValue, "XmlSchemaString", "Google"));
                        }
                    }
                }
            };
            googleAuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.login email");
            app.UseGoogleAuthentication(googleAuthenticationOptions);




            UserManagerFactory = () =>
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
