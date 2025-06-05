using System.ComponentModel.DataAnnotations;

namespace DocNotifyAPI.Models
{


    public class UserRegisterRequest
    {
       [Required(ErrorMessage = "Username is manditory")]
        [MinLength(5,ErrorMessage ="Invalid entry for username")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is manditory")]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}