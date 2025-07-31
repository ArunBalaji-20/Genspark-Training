
using ChienVHShopOnline.Models;

public interface IProductRepository
{
    Task<(IEnumerable<Product>, int totalCount)> GetPagedAsync(int page, int pageSize);

}