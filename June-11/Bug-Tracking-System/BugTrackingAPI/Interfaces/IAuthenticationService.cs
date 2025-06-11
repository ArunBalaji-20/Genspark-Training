

using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Models.DTOs;

namespace BugTrackingAPI.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponse> Login(UserLoginRequest user);

        public Task<UserLoginResponse> Register(UserRegisterRequest user);

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken);

        public Task<string> LogOut(LogOutRequest request,string email);
    }
}