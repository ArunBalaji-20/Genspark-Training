
using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repositories
{
    public class EmployeeRepository : Repository<long, Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BugContext context) : base(context)
        {

        }



        public override async Task<Employee> Get(long key)
        {
            return await _bugContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == key);
        }

        public override async Task<IEnumerable<Employee>> GetAll()
        {
            return await _bugContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await _bugContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        
    }
}