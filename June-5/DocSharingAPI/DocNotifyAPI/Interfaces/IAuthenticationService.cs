

using DocNotifyAPI.Models;
using DocNotifyAPI.Models.DTO;

namespace DocNotifyAPI.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponse> Login(UserLoginRequest user);

        public Task<UserLoginResponse> Register(UserRegisterRequest user);
    }
}