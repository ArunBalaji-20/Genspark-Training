namespace BugTrackingAPI.Models.DTO
{
    public class BugResponse
    {
        public long BugId { get; set; }
        public string BugName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Screenshot { get; set; } = string.Empty;
        public float CvssScore { get; set; }
        public DateTime SubmittedOn { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public long SubmittedById { get; set; }
    }
}