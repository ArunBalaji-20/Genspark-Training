

using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface ITokenService
    {
        public Task<TokenResponse> GenerateToken(Employee emp);
        // public  GenerateRefreshToken();
    }
}