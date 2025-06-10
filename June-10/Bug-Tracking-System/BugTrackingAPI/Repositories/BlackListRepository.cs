using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BugTrackingAPI.Context;

namespace BugTrackingAPI.Repositories
{
    public class BlackListRepository : Repository<long, BlackListedToken>,IBlackListRepository
    {

        public BlackListRepository(BugContext context) : base(context)
        {
        }

       

        public override Task<BlackListedToken> Get(long key)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<BlackListedToken>> GetAll()
        {
            throw new NotImplementedException();
        }

        // public async Task AddAsync(BlacklistedToken token)
        // {
        //     _context.BlacklistedTokens.Add(token);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<bool> IsBlacklistedAsync(string token)
        {
            return await _bugContext.BlackListedTokens
                .AnyAsync(e => e.Token == token);

                
        }
    }
}