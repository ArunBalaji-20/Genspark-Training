using BugTrackingAPI.Context;
using BugTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repositories
{
    public class BugCommentRepository : Repository<long, BugComment>
    {
         public BugCommentRepository(BugContext context) : base(context)
        {

        }
        public override async Task<BugComment> Get(long key)
        {
              return await _bugContext.BugComments.FirstOrDefaultAsync(e => e.CommentId == key);
        }

        public override async Task<IEnumerable<BugComment>> GetAll()
        {
            return await _bugContext.BugComments.ToListAsync();
        }
    }
}