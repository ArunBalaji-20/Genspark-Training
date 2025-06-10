using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models
{
    public class BlackListedToken
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}