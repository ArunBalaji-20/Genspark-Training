namespace BugTrackingAPI.Models.DTO
{
    public class BugAssignmentResponse
    {
        public long AssignmentId { get; set; }

        public DateTime AssignedDate { get; set; }

        public string ResolutionStatus { get; set; }   =string.Empty;


        public long BugId { get; set; }
        public BugResponse? Bug { get; set; }

        public long DevId { get; set; }

        public long AssignedById { get; set; }
    }
}