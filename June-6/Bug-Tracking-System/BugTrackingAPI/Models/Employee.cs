using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Models
{
    public class Employee
    {
        [Key]
        public  long EmployeeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // Admin ,Developer,Tester

        public string Email { get; set; } = string.Empty;


        public string PasswordHash { get; set; } = string.Empty;


        public ICollection<Bugs>? SubmittedBugs { get; set; } // by tester
        public ICollection<BugAssignment>? AssignedBugs { get; set; } //to dev
        public ICollection<BugAssignment>? AssignedByAdmin { get; set; } // ← maps to Admin

    }
}