using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ViewModels
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 80 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Login length must be between 3 and 30 characters")]
        public string Login { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; }

        [Required(ErrorMessage = "Account status is required")]
        public AccountStatus AccountStatus { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}