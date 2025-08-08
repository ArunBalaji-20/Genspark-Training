
using System;
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class ColorsRepository:Repository<int,Color>
{
     public ColorsRepository(ChienVHShopDBEntities options ) : base(options)
    {

    }

    

    public override async Task<Color> Get(int key)
    {
        // throw new NotImplementedException();
        return await _ChienVHShopDBEntities.Colors
        .FirstOrDefaultAsync(e => e.ColorId == key);
       

    }

    public override async Task<IEnumerable<Color>> GetAll()
    {
        return await _ChienVHShopDBEntities.Colors
        .Include(e=>e.Products)
        .ToListAsync();
    }
}
