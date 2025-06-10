

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingAPI.Models
{
    public class RefreshTokenModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RefreshToken { get; set; } = string.Empty;

        public DateTime RefreshTokenExpiryTime { get; set; }

        // Foreign Key to Employee
        [Required]
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;
    }
}
