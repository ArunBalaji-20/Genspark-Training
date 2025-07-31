using System;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Interfaces;

public interface IProductService
{
    Task<PagedResponse<Product>> GetPagedAsync(int page, int pageSize);
    Task<Product> GetByIdAsync(int id);
}
