using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models.DTO
{
    public class BugCommentRequest
    {

        [Required(ErrorMessage = "comment  is mandatory")]
        public string Comment { get; set; } = string.Empty;

        [Required(ErrorMessage = "bugId is mandatory")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid Bug ID")]
        public long BugId { get; set; }

    }
}