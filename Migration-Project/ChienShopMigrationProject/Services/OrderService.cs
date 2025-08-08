using System;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using ChienShopMigrationProject.Repositories;
namespace ChienShopMigrationProject.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<int, Order> _orderRepository;
    private readonly IOrderRepository _orderRepository1;
    private readonly IRepository<int,Product> _productRepository;

    public OrderService(IRepository<int, Order> orderRepository,
                        IOrderRepository orderRepository1,
                        IRepository<int,Product> productRepository)
    {
        _orderRepository = orderRepository;

        _orderRepository1 = orderRepository1;

        _productRepository = productRepository;
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

    public async Task<Order> AddAsync(OrderCreateDto dto)
    {
        foreach (var productId in dto.OrderDetails.Select(d => d.ProductID))
        {
            var product = await _productRepository.Get(productId);
            if (product == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }
        }
        var orderDetails = await Task.WhenAll(dto.OrderDetails.Select(async d => new OrderDetail
        {
            ProductID = d.ProductID,
            Quantity = d.Quantity,
            Price = await GetUnitPrice(d.ProductID)
        }));

        var order = new Order
        {
            OrderName = dto.OrderName,
            OrderDate = dto.OrderDate,
            PaymentType = dto.PaymentType,
            Status = dto.Status,
            CustomerName = dto.CustomerName,
            CustomerPhone = dto.CustomerPhone,
            CustomerEmail = dto.CustomerEmail,
            CustomerAddress = dto.CustomerAddress,
            OrderDetails = orderDetails.ToList()
        };
        await _orderRepository.Add(order);

        return order;

        
    }

    private async Task<double?> GetUnitPrice(int id)
    {
        var product = await _productRepository.Get(id);
        var price = product.Price;
        return price;
    }

    public async Task UpdateAsync(Order order)
    {
        await _orderRepository.Update(order.OrderID, order);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.Delete(id);
    }

    public async Task<string> CreatePaymentSession(int orderId)
    {
        var order = await _orderRepository.Get(orderId);
        if (order == null)
        {
            throw new KeyNotFoundException("invalid id");
        }


        var sessionId = Guid.NewGuid().ToString();

        order.PaymentSessionId = sessionId;
        await _orderRepository.Update(orderId, order);

        return sessionId;
    }


    public async Task MarkOrderAsPaidAsync(string paymentSessionId)
    {
        var all = await _orderRepository.GetAll();

        var order = all.FirstOrDefault(o => o.PaymentSessionId == paymentSessionId);

        if (order == null)
        {
            throw new Exception("Invalid key");
        }
        order.Status = "Paid";
        await _orderRepository.Update(order.OrderID, order);
    }

    // public async Task<byte[]> ExportToPdf()
    // {
    //     var orderList = await _orderRepository.GetAll();
    //     var strw = new StringWriter();

    //     strw.WriteLine("\"OrderID\",\"OrderName\",\"OrderDate\",\"PaymentType\",\"Status\",\"CustomerName\",\"CustomerPhone\",\"CustomerEmail\",\"CustomerAddress\",\"PaymentSessionId\"");

    //     foreach (var order in orderList)
    //     {
    //         strw.WriteLine($"\"{order.OrderID}\",\"{order.OrderName}\",\"{order.OrderDate:yyyy-MM-dd}\",\"{order.PaymentType}\",\"{order.Status}\",\"{order.CustomerName}\",\"{order.CustomerPhone}\",\"{order.CustomerEmail}\",\"{order.CustomerAddress}\",\"{order.PaymentSessionId}\"");
    //     }
    //     var bytes = System.Text.Encoding.UTF8.GetBytes(strw.ToString());
    //     return bytes;
    // }



public async Task<byte[]> ExportToPdf()
{
    var orderList = await _orderRepository.GetAll();
    QuestPDF.Settings.License = LicenseType.Community;


    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Size(PageSizes.A4);

            page.Content().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(60); // OrderID
                    columns.ConstantColumn(80); // OrderName
                    columns.ConstantColumn(60); // OrderDate
                    columns.ConstantColumn(70); // PaymentType
                    columns.ConstantColumn(50); // Status
                    columns.ConstantColumn(70); // CustomerName
                    // columns.ConstantColumn(60); //CustomerPhone
                    columns.ConstantColumn(70); //CustomerEmail
                    // columns.ConstantColumn(60); //CustomerAddress
                    columns.ConstantColumn(70); //PaymentSessionId
                });

                // Header
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text(x => x.Span("OrderID"));
                    header.Cell().Element(CellStyle).Text(x=> x.Span("OrderName"));
                    header.Cell().Element(CellStyle).Text(x=> x.Span("Order Date"));
                    header.Cell().Element(CellStyle).Text(x => x.Span("Payment Type"));
                    header.Cell().Element(CellStyle).Text(x => x.Span("Status"));
                    header.Cell().Element(CellStyle).Text(x => x.Span("CustomerName"));
                    // header.Cell().Element(CellStyle).Text(x => x.Span("CustomerPhone"));
                    header.Cell().Element(CellStyle).Text(x => x.Span("Customer Email"));
                    // header.Cell().Element(CellStyle).Text(x => x.Span("CustomerAddress"));
                    header.Cell().Element(CellStyle).Text(x => x.Span("PaymentSessionId")); 
                    // header.Cell().Element(CellStyle).Text("PaymentType");
                    // header.Cell().Element(CellStyle).Text("Status");
                    // header.Cell().Element(CellStyle).Text("CustomerName");
                    // header.Cell().Element(CellStyle).Text("CustomerPhone");
                    // header.Cell().Element(CellStyle).Text("CustomerEmail");
                    // header.Cell().Element(CellStyle).Text("CustomerAddress");
                    // header.Cell().Element(CellStyle).Text("PaymentSessionId");


                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.Bold()).Padding(5).Background("#EEE");
                    }
                });

                // Data
                foreach (var order in orderList)
                {
                    table.Cell().Element(CellStyle).Text(order.OrderID.ToString());
                    table.Cell().Element(CellStyle).Text(order.OrderName ?? "");
                    table.Cell().Element(CellStyle).Text(order.OrderDate.ToString());
                    table.Cell().Element(CellStyle).Text(order.PaymentType ?? "");
                    table.Cell().Element(CellStyle).Text(order.Status ?? "");
                    table.Cell().Element(CellStyle).Text(order.CustomerName ?? "");
                    // table.Cell().Element(CellStyle).Text(order.CustomerPhone ?? "");    
                    table.Cell().Element(CellStyle).Text(order.CustomerEmail ?? "");
                    // table.Cell().Element(CellStyle).Text(order.CustomerAddress ?? "");
                    table.Cell().Element(CellStyle).Text(order.PaymentSessionId ?? "");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.Padding(5);
                    }
                }
            });
        });
    });

    return document.GeneratePdf();
}


}
