using System;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class CategoryRepository : Repository<int, Category>
{
    public CategoryRepository(ChienVHShopDBEntities options) : base(options)
    {

    }



    public override async Task<Category> Get(int key)
    {
        // throw new NotImplementedException();
        return await _ChienVHShopDBEntities.Categories
        .FirstOrDefaultAsync(e => e.CategoryId == key);

    }

    public override async Task<IEnumerable<Category>> GetAll()
    {
        return await _ChienVHShopDBEntities.Categories
        .ToListAsync();
    }
}
