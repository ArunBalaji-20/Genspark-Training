using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingAPI.Models
{
    public class BugAssignment
    {
        [Key]
        public long AssignmentId { get; set; }

        public DateTime AssignedDate { get; set; }

        [Required]
        public string ResolutionStatus { get; set; }   =string.Empty;

        public DateTime? ResolvedOn { get; set; }

        // Foreign Keys
        [ForeignKey("Bug")]
        public long BugId { get; set; }
        public Bugs? Bug { get; set; }

        public long DevId { get; set; }
        public Employee? Developer { get; set; }

        public long AssignedById { get; set; }
        public Employee? Admin { get; set; }
    }
}