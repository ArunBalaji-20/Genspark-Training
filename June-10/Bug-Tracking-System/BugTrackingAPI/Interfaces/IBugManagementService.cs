using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IBugManagementService
    {
        public Task<BugAssignmentResponse> AssignBugToDevs(long bugId, long devId, string email);

        public Task<BugAssignmentResponse> ResolveBugs(long bugId, string email);
    }
}