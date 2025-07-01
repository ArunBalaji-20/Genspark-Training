using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IEmployeeManagementService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllEmployees();
        Task<string> DeleteEmployee(long employeeId);

        // Task<string> GetEmployeeStats(long id, string email);
        public  Task<EmployeeResponse> GetEmployeeDetails(long id);
    }
}
