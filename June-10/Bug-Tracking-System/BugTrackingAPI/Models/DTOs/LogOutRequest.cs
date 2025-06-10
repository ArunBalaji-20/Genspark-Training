using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models.DTOs;
public class LogOutRequest
{
    [Required(ErrorMessage = "Refresh token is mandatory")]
    public string RefreshToken { get; set; }
}