using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repositories
{
    public class BugsAssignmentRepository : Repository<long, BugAssignment>,IBugManagementRepository
    {
        public BugsAssignmentRepository(BugContext context) : base(context)
        {

        }

        public override async Task<BugAssignment> Get(long key)
        {
            return await _bugContext.BugAssignments.FirstOrDefaultAsync(e => e.BugId == key);
        }

        public override async Task<IEnumerable<BugAssignment>> GetAll()
        {
            return await _bugContext.BugAssignments.ToListAsync();

        }

        public async Task<IEnumerable<BugResponse>> GetBugsAssignedToMe(long devId)
        {
            var bugs = await _bugContext
            .Database
            .SqlQueryRaw<BugResponse>("SELECT * FROM GetBugsAssignedToDev({0});", devId)
            .ToListAsync();

            return bugs;
        }
    }
}