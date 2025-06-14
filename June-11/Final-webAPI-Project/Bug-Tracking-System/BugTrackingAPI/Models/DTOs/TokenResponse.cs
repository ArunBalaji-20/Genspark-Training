
namespace BugTrackingAPI.Models.DTO
{
    public class TokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}