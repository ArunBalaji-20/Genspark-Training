using System;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;

namespace ChienShopMigrationProject.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateCategory(CategoryAddDto name);
    Task<Category> GetByIdAsync(int id);
    Task UpdateAsync(CategoryAddDto name, int id);
    Task DeleteAsync(int id);
}
