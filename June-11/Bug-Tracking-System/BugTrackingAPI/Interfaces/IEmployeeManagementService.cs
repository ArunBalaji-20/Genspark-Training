using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IEmployeeManagementService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllEmployees();
        Task<string> DeleteEmployee(long employeeId);
    }
}
