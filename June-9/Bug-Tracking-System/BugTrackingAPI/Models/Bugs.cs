using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingAPI.Models
{
    public class Bugs
    {

        [Key]
        public long BugId { get; set; }

        [Required]
        public string BugName { get; set; } =string.Empty;

        public string Description { get; set; } =string.Empty;

        public string Screenshot { get; set; }=string.Empty;

        public float CvssScore { get; set; }

        public DateTime SubmittedOn { get; set; }

        public DateTime? ResolvedAt { get; set; }

        [Required]
        public string Status { get; set; } =string.Empty;// resolved or ongoing

        // Foreign Key
        [ForeignKey("SubmittedBy")]
        public long SubmittedById { get; set; }
        public Employee? SubmittedBy { get; set; }

        // Navigation
        public ICollection<BugAssignment>? Assignments { get; set; }
        public ICollection<BugComment>? Comments { get; set; }

       
    }
}