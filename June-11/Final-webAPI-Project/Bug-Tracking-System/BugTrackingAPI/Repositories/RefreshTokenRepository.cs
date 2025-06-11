
using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repositories
{
    public class RefreshTokenRepository : Repository<int, RefreshTokenModel>,IRefreshTokenRepository
    {
        public RefreshTokenRepository(BugContext context) : base(context)
        {

        }


        public override async Task<RefreshTokenModel> Get(int key)
        {
            var result = await _bugContext.RefreshTokens.FirstOrDefaultAsync(e => e.Id == key);
            return result;
        }

        public override async Task<IEnumerable<RefreshTokenModel>> GetAll()
        {
            var result = await _bugContext.RefreshTokens.ToListAsync();
            return result;
        }

        public async Task<RefreshTokenModel> GetByToken(string token)
        {
            var result = await _bugContext.RefreshTokens.FirstOrDefaultAsync(e => e.RefreshToken == token);
            return result;
        }
    }
}