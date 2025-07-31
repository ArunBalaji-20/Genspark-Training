
using System;
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class ContactUsRepository:Repository<int,ContactU>
{
     public ContactUsRepository(ChienVHShopDBEntities options ) : base(options)
    {

    }

    

    public override async Task<ContactU> Get(int key)
    {
        // throw new NotImplementedException();
        return await _ChienVHShopDBEntities.ContactUs
        .FirstOrDefaultAsync(e => e.id == key);
       

    }

    public override async Task<IEnumerable<ContactU>> GetAll()
    {
        return await _ChienVHShopDBEntities.ContactUs
        .ToListAsync();
    }
}
