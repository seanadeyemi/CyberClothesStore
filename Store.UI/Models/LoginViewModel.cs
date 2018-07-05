using System.ComponentModel.DataAnnotations;

namespace Store.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6)]

        public string Password { get; set; }


    }
}