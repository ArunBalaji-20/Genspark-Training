using BugTrackingAPI.Models;

namespace BugTrackingAPI.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<int, RefreshTokenModel>
    {
        Task<RefreshTokenModel> GetByToken(string token);
    }
}
