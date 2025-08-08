using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IBugManagementService
    {
        public Task<BugAssignmentResponse> AssignBugToDevs(long bugId, long devId, string email);

        public Task<BugAssignmentResponse> ResolveBugs(long bugId, string email);

        public Task<BugAssignmentResponse> UpdateInProgress(long bugId, string email);

        public Task<IEnumerable<AssignedResponse>> GetAssignedList();

        public Task<IEnumerable<AvailableDev>> GetAvailableDevs();

        public Task<IEnumerable<BugResponse>> GetBugsAssignedToMe(string email);

        public Task<IEnumerable<BugResponse>> GetBugsAssignedToDev(long empId);

        public  Task<int> GetOngoingTaskList(long id);
    }
}