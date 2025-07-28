using System;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class UserRepository: Repository<int,User>
{
    public UserRepository(ChienVHShopDBEntities options ) : base(options)
    {

    }

    public override async Task<User> Get(int key)
    {
        // throw new NotImplementedException();
        return await _ChienVHShopDBEntities.Users.FirstOrDefaultAsync(e => e.UserId == key);
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        return await _ChienVHShopDBEntities.Users.ToListAsync();
    }
    
}
