using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IBugService
    {
        public Task<BugSubmission> SubmitBug(BugSubmission bug, string email);

        public Task<IEnumerable<BugResponse>> GetAllBugsList();
        public  Task<IEnumerable<BugResponse>> GetAllBugsInSubmittedState();

        public Task<IEnumerable<BugResponse>> GetBugsReportedBy(long empId);

        public Task<BugResponse> GetBugStatus(long BugId);
    }
}