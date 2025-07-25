using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models.DTO
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "email is mandatory")]
        [MinLength(5, ErrorMessage = "Invalid entry for email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; } = string.Empty;
    }
}