
namespace BugTrackingAPI.Models.DTO
{
    public class EmployeeResponse
    {
        public  long EmployeeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // Admin ,Developer,Tester

        public string Email { get; set; } = string.Empty;
    }
}