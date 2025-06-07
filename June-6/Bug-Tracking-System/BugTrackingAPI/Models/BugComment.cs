using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BugTrackingAPI.Models
{
    public class BugComment
    {
        [Key]
        public long CommentId { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        public DateTime CommentedOn { get; set; }

        // Foreign Keys
        [ForeignKey("Bug")]
        public long BugId { get; set; }
        public Bugs? Bug { get; set; }

        [ForeignKey("Commenter")]
        public long CommenterId { get; set; }
        public Employee? Commenter { get; set; }
    }
}
