using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repositories
{
    public class BugsAssignmentRepository : Repository<long, BugAssignment>
    {
        public BugsAssignmentRepository(BugContext context) : base(context)
        {

        }

        public override async Task<BugAssignment> Get(long key)
        {
            return await _bugContext.BugAssignments.FirstOrDefaultAsync(e => e.AssignmentId == key);
        }

        public override async Task<IEnumerable<BugAssignment>> GetAll()
        {
            return await _bugContext.BugAssignments.ToListAsync();

        }

        

    }
}