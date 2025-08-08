using System;
using ChienShopMigrationProject.Dtos;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Interfaces;


public interface IOrderService
{
    Task<PagedResponse<Order>> GetPagedAsync(int page, int pageSize);
    Task<Order?> GetByIdAsync(int id);
    Task<Order> AddAsync(OrderCreateDto dto);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);

    Task<string> CreatePaymentSession(int orderId);

    Task MarkOrderAsPaidAsync(string paymentSessionId);

    Task<byte[]> ExportToPdf();
}
