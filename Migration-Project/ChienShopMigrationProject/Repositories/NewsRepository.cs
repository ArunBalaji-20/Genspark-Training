
using System;
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repository;
using ChienVHShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChienShopMigrationProject.Repositories;

public class NewsRepository:Repository<int,News>,INewsRepository
{
     public NewsRepository(ChienVHShopDBEntities options ) : base(options)
    {

    }

    public async Task<(IEnumerable<News>, int totalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _ChienVHShopDBEntities.News
            .Where(o => o.Status == 0)
            .OrderByDescending(o => o.NewsId);
        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, totalCount);
    }

    public override async Task<News> Get(int key)
    {
        // throw new NotImplementedException();
        return await _ChienVHShopDBEntities.News
        .Where(e=>e.Status==0)
        .FirstOrDefaultAsync(e => e.NewsId == key);
       

    }

    public override async Task<IEnumerable<News>> GetAll()
    {
        return await _ChienVHShopDBEntities.News
        .Where(e=>e.Status==0)
        .ToListAsync();
    }
}
