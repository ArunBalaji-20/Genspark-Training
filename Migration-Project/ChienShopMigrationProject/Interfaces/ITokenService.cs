



using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;

namespace ChienShopMigrationProject.Interfaces
{
    public interface ITokenService
    {
        public Task<TokenResponse> GenerateToken(User user);
    }
}