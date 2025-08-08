using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IRepository<long, Employee> _employeeRepository;
        private readonly IRepository<long, BugComment> _BugCommentRepo;
        private readonly IBugManagementRepository _bugManagementRepository;

        public EmployeeManagementService(IRepository<long, Employee> employeeRepository,
                                        IRepository<long, BugComment> BugCommentRepo,
                                         IBugManagementRepository bugManagementRepository)
        {
            _employeeRepository = employeeRepository;
            _BugCommentRepo = BugCommentRepo;
            _bugManagementRepository = bugManagementRepository;
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
            var emp = await _employeeRepository.Get(employeeId);
            if (emp == null)
            {
                throw new KeyNotFoundException("Employee not found or already deleted.");
            }
            emp.IsDeleted = true;
            await _employeeRepository.Update(employeeId, emp);
            // var deletedEmployee = await _employeeRepository.Delete(employeeId);
            

            return "Employee Deleted Successfully";
        }

        public async Task<EmployeeResponse> GetEmployeeDetails(long id)
        {
            var empDetails = await _employeeRepository.Get(id);
            if (empDetails == null)
            {
                throw new KeyNotFoundException("employee not found");
            }
            var result = new EmployeeResponse
            {
                EmployeeId = empDetails.EmployeeId,
                Email = empDetails.Email,
                Role = empDetails.Role,
                Name = empDetails.Name
            };
            return result;

            // var Comments = await _BugCommentRepo.GetAll();

            // var latestComments = Comments.Where(e=>e.CommenterId==id).Select()
            // var latestComments = Comments
            // .Where(e => e.CommenterId == id)
            // .OrderByDescending(e => e.CommentedOn)
            // .Take(3)
            // .ToList();

            // if (empDetails.Role == "Dev")
            // {
            //      var bugs = await _bugManagementRepository.GetBugsAssignedToMe(id);
            // }
            // Console.WriteLine(latestComments);
            // return "done";
        }
    }

    
}
