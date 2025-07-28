using ChienVHShopOnline.Models;

public interface IOrderRepository
{
    Task<(IEnumerable<Order>, int totalCount)> GetPagedAsync(int page, int pageSize);

}