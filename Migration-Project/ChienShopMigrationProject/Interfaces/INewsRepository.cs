using System;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Interfaces;

public interface INewsRepository
{
        Task<(IEnumerable<News>, int totalCount)> GetPagedAsync(int page, int pageSize);

}
