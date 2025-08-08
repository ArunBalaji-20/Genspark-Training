using System;
using ChienShopMigrationProject.Dtos;
using ChienVHShopOnline.Dtos;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Interfaces;


public interface INewsService
{
    Task<PagedResponse<News>> GetPagedAsync(int page, int pageSize);
    Task<News> GetByIdAsync(int id);

    Task PostNews(NewsCreationDto news);
    Task UpdateAsync(NewsCreationDto news, int id);

    Task DeleteAsync(int id);

    Task<byte[]> ExportToCSV();

    Task<byte[]> ExportToCExcel();
}
