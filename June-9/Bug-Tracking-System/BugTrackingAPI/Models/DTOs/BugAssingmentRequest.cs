// Models/DTO/AssignBugRequest.cs
namespace BugTrackingAPI.Models.DTO
{
    public class BugAssignmentRequest
    {
        public long BugId { get; set; }
        public long DevId { get; set; }
    }
}
