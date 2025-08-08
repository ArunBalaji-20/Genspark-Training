using System;
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class OrderRepository:Repository<int,Order>,IOrderRepository
{
     public OrderRepository(ChienVHShopDBEntities options ) : base(options)
    {

    }

    public async Task<(IEnumerable<Order>, int totalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _ChienVHShopDBEntities.Orders.OrderByDescending(o => o.OrderID);
        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, totalCount);
    }

    public override async Task<Order> Get(int key)
    {
        // throw new NotImplementedException();
        return await _ChienVHShopDBEntities.Orders.
        Include(e=>e.OrderDetails).
        FirstOrDefaultAsync(e => e.OrderID == key);
    }

    public override async Task<IEnumerable<Order>> GetAll()
    {
        return await _ChienVHShopDBEntities.Orders.ToListAsync();
    }
}
