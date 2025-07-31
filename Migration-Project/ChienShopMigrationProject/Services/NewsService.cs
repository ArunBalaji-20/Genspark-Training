using System;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Dtos;
using ChienVHShopOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Services;

public class NewsService : INewsService
{
    private readonly IRepository<int, News> _newsRepo;
    private readonly INewsRepository _newsRepo1;

    public NewsService(IRepository<int, News> newsRepo, INewsRepository newsRepo1)
    {
        _newsRepo = newsRepo;

        _newsRepo1 = newsRepo1;
    }


    public async Task<News> GetByIdAsync(int id)
    {
        return await _newsRepo.Get(id);
    }

    public async Task<PagedResponse<News>> GetPagedAsync(int page, int pageSize)
    {
        var (news, totalCount) = await _newsRepo1.GetPagedAsync(page, pageSize);
        return new PagedResponse<News>(news, page, pageSize, totalCount);
    }

    public async Task PostNews(NewsCreationDto news)
    {
        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "NewsImages");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(news.Image.FileName);
        string filePath = Path.Combine(uploadsFolder, fileName);


        var stream = new FileStream(filePath, FileMode.Create);
        await news.Image.CopyToAsync(stream);
        stream.Close();

        var NewNews = new News
        {
            UserId = news.UserId,
            Title = news.Title,
            ShortDescription = news.ShortDescription,
            Image = news.Image.FileName,
            Content = news.Content,
            CreatedDate = DateTime.UtcNow,
            Status = 0
        };

        await _newsRepo.Add(NewNews);
    }

    public async Task UpdateAsync(NewsCreationDto news, int id)
    {
        var GetNews = await _newsRepo.Get(id);
        if (GetNews == null)
        {
            throw new KeyNotFoundException("Invalid id");
        }
        GetNews.Content = news.Content;
        GetNews.Title = news.Title;
        GetNews.Image = news.Image.FileName;
        GetNews.ShortDescription = news.ShortDescription;

        await _newsRepo.Update(id, GetNews);
    }

    public async Task DeleteAsync(int id) //soft delete
    {
        var GetNews = await _newsRepo.Get(id);
        if (GetNews == null)
        {
            throw new KeyNotFoundException("Invalid id");
        }
        GetNews.Status = 1;
        await _newsRepo.Update(id, GetNews);

    }

    public async Task<byte[]> ExportToCSV()
    {

        var listNews = await _newsRepo.GetAll();
        var strw = new StringWriter();


        strw.WriteLine("\"NewsId\",\"Title\",\"ShortDescription\",\"CreatedDate\",\"Status\"");

        
        foreach (var news in listNews)
        {
            strw.WriteLine($"\"{news.NewsId}\",\"{news.Title}\",\"{news.ShortDescription}\",\"{news.CreatedDate:yyyy-MM-dd}\",\"{news.Status}\"");
        }

        var bytes = System.Text.Encoding.UTF8.GetBytes(strw.ToString());

        return bytes;

     }
}
