
namespace BugTrackingAPI.Models.DTO
{
    public class BugSubmission
    {

        public string BugName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public required IFormFile Screenshot { get; set; } 

        public float CvssScore { get; set; }

        // public DateTime SubmittedOn { get; set; }
        //public DateTime? ResolvedAt { get; set; }

        // [Required]
        // public string Status { get; set; } =string.Empty;// resolved or ongoing

        // public long SubmittedById { get; set; }


    }
}