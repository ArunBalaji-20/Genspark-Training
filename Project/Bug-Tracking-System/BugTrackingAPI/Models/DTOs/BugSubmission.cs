
using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models.DTO
{
    public class BugSubmission
    {
        [Required(ErrorMessage = "Bug Name token is mandatory")]
        public string BugName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "screenshot  is mandatory")]
        public required IFormFile Screenshot { get; set; } 

        public float CvssScore { get; set; }



    }
}