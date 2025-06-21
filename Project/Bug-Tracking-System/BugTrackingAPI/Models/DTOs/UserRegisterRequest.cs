using System.ComponentModel.DataAnnotations;
using FirstAPI.Misc;

namespace BugTrackingAPI.Models.DTO
{


    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Name is mandatory")]
        [MinLength(3, ErrorMessage = "Invalid entry for name")]
        [NameValidation(ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "email is mandatory")]
        [MinLength(5, ErrorMessage = "Invalid entry for email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; 

    }
}

