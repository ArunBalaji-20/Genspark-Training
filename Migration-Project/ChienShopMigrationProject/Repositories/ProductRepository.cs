
using System;
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class ProductRepository:Repository<int,Product>,IProductRepository
{
     public ProductRepository(ChienVHShopDBEntities options ) : base(options)
    {

    }

    public async Task<(IEnumerable<Product>, int totalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _ChienVHShopDBEntities.Products.OrderByDescending(o => o.ProductId);
        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, totalCount);
    }

    public override async Task<Product> Get(int key)
    {
        // throw new NotImplementedException();
        // return await _ChienVHShopDBEntities.Products.FirstOrDefaultAsync(e => e.ProductId == key);
        return await _ChienVHShopDBEntities.Products
        .Include(p => p.Category)
        .Include(p => p.Color)
        .Include(p => p.Model)
        .Include(p => p.OrderDetails)
        .FirstOrDefaultAsync(e => e.ProductId == key);

    }

    public override async Task<IEnumerable<Product>> GetAll()
    {
        return await _ChienVHShopDBEntities.Products
        .Include(p => p.Category)
        .Include(p => p.Color)
        .Include(p => p.Model)
        .Include(p => p.OrderDetails)
        .ToListAsync();
    }
}
