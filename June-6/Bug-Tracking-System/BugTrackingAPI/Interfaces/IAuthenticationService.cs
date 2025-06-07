

using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponse> Login(UserLoginRequest user);

        public Task<UserLoginResponse> Register(UserRegisterRequest user);

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken);
    }
}