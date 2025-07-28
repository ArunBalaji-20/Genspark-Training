using System;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Interfaces;


public interface IOrderService
{
    Task<PagedResponse<Order>> GetPagedAsync(int page, int pageSize);
    Task<Order?> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
}
