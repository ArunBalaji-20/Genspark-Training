using System;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Services;

public class ProductService : IProductService
{
    private readonly IRepository<int, Product> _productRespository;
    private readonly IProductRepository _productRepository1;

    public ProductService(IRepository<int, Product> productRepository, IProductRepository productRepository1)
    {
        _productRespository = productRepository;

        _productRepository1 = productRepository1;
    }

     public async Task<PagedResponse<Product>> GetPagedAsync(int page, int pageSize)
    {
        var (products, totalCount) = await _productRepository1.GetPagedAsync(page, pageSize);
        return new PagedResponse<Product>(products, page, pageSize, totalCount);
    }
    public async Task<Product> GetByIdAsync(int id)
    {
        return await _productRespository.Get(id);
    }

   
}
