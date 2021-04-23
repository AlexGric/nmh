using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Login length must be between 3 and 30 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 30 characters")]
        public string Password { get; set; }
    }
}