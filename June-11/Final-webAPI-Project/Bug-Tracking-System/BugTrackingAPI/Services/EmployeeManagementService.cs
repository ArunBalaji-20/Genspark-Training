using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IRepository<long, Employee> _employeeRepository;

        public EmployeeManagementService(IRepository<long, Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployees()
        {
            var allEmployees = await _employeeRepository.GetAll();

            var result = allEmployees.Select(emp => new EmployeeResponse
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name,
                Email = emp.Email,
                Role = emp.Role
            }).ToList();

            return result;
        }

        public async Task<string> DeleteEmployee(long employeeId)
        {
            var deletedEmployee = await _employeeRepository.Delete(employeeId);
            if (deletedEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found or already deleted.");
            }

            return "Employee Deleted Successfully";
        }
    }
}
