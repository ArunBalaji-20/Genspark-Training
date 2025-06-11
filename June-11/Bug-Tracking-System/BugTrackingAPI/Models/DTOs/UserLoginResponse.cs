namespace BugTrackingAPI.Models.DTO
{
    public class UserLoginResponse
    {
        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}