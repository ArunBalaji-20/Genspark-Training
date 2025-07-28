using System;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<int, Order> _orderRepository;
    private readonly IOrderRepository _orderRepository1;

    public OrderService(IRepository<int, Order> orderRepository, IOrderRepository orderRepository1)
    {
        _orderRepository = orderRepository;

        _orderRepository1 = orderRepository1;
    }

    
     public async Task<PagedResponse<Order>> GetPagedAsync(int page, int pageSize)
    {
        var (orders, totalCount) = await _orderRepository1.GetPagedAsync(page, pageSize);
        return new PagedResponse<Order>(orders, page, pageSize, totalCount);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _orderRepository.Get(id);
    }

    public async Task AddAsync(Order order)
    {
        await _orderRepository.Add(order);
    }

    public async Task UpdateAsync(Order order)
    {
        await _orderRepository.Update(order.OrderID, order);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.Delete(id);
    }
}
