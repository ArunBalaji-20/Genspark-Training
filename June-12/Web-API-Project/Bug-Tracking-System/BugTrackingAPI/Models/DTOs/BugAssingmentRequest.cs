// Models/DTO/AssignBugRequest.cs
using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models.DTO
{
    public class BugAssignmentRequest
    {
        [Required(ErrorMessage = "bugId is mandatory")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid Bug ID")]
        public long BugId { get; set; }
        
        [Required(ErrorMessage = "devId is mandatory")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid Dev ID")]
        public long DevId { get; set; }
    }
}
