using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models.DTO
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "email is manditory")]
        [MinLength(5,ErrorMessage ="Invalid entry for email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is manditory")]
        public string Password { get; set; } = string.Empty;
    }
}