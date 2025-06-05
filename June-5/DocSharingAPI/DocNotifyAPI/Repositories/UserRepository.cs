
using DocNotifyAPI.Context;
using DocNotifyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocNotifyAPI.Repositories
{
    public class UserRepository : Repository<string, User>
    {
        public UserRepository(DocContext context):base(context)
        {
            
        }
        public override async Task<User> Get(string key)
        {
            return await _DocContext.Users.SingleOrDefaultAsync(u => u.Username == key);
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _DocContext.Users.ToListAsync();
        }
            
    }
}