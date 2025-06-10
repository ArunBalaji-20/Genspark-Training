using BugTrackingAPI.Models;

namespace BugTrackingAPI.Interfaces
{
    public interface IEmployeeRepository : IRepository<long, Employee>
    {
        public Task<Employee?> GetEmployeeByEmail(string email);
    }
}

