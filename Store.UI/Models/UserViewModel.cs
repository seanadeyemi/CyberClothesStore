using ClassLibrary1;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Store.UI.Models
{
    public class UserViewModel
    {

        public static implicit operator UserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed
                
                
            };
        }
        //
        // Summary:
        //     Email
        public string Email { get; set; }
        //
        // Summary:
        //     True if the email is confirmed, default is false
        public bool EmailConfirmed { get; set; }

        //
        // Summary:
        //     A random value that should change whenever a users credentials have changed (password
        //     changed, login removed)
        //public virtual string SecurityStamp { get; set; }
        //
        // Summary:
        //     PhoneNumber for the user
        public string PhoneNumber { get; set; }

        //
        // Summary:
        //     Is two factor enabled for the user
        //public virtual bool TwoFactorEnabled { get; set; }
        //
        // Summary:
        //     DateTime in UTC when lockout ends, any time in the past is considered not locked
        //     out.
        //public virtual DateTime? LockoutEndDateUtc { get; set; }
        //
        // Summary:
        //     Is lockout enabled for this user
        //public virtual bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Used to record failures for the purposes of lockout
        //public virtual int AccessFailedCount { get; set; }
        //
        // Summary:
        //     Navigation property for user roles
        public virtual ICollection<IRole> Roles { get; }
        //
        // Summary:
        //     Navigation property for user claims
        public virtual ICollection<Claim> Claims { get; }
        //
        // Summary:
        //     Navigation property for user logins
      //  public virtual ICollection<TLogin> Logins { get; }
        //
        // Summary:
        //     User ID (Primary Key)
        public string Id { get; set; }
        //
        // Summary:
        //     User name
        public string UserName { get; set; }

        public string Role { get; set; }

    }
}