using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repositories
{
    public class BugsRepository : Repository<long, Bugs>
    {
        public BugsRepository(BugContext context) : base(context)
        {

        }

        public override async Task<Bugs> Get(long key)
        {
            return await _bugContext.Bugs.FirstOrDefaultAsync(e => e.BugId == key);
        }

        public override async Task<IEnumerable<Bugs>> GetAll()
        {
            return await _bugContext.Bugs.ToListAsync();
        }
    }
}