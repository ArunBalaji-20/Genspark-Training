
using DocNotifyAPI.Models;

namespace DocNotifyAPI.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}