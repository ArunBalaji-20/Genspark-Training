using System;
using System.Threading.Tasks;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Dtos;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Services;

public class CategoryService : ICategoryService
{

    private readonly IRepository<int, Category> _CategoryRepo;

    public CategoryService(IRepository<int, Category> CategoryRepo)
    {
        _CategoryRepo = CategoryRepo;


    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        var res = await _CategoryRepo.GetAll();
        return res;
    }
    public async Task<Category> GetByIdAsync(int id)
    {
        var res = await _CategoryRepo.Get(id);
        if (res == null)
        {
            throw new KeyNotFoundException("invalid id");
        }
        return res;
    }

    public async Task<Category> CreateCategory(CategoryAddDto categoryAddDto)
    {
        var all = await _CategoryRepo.GetAll();
        bool checkExist = all.Any(e => e.Name == categoryAddDto.Name);
        if (checkExist)
        {
            throw new Exception("Category already present");
        }
        var newCategory = new Category
        {
            Name = categoryAddDto.Name
        };

        await _CategoryRepo.Add(newCategory);
        return newCategory;

    }

    public async Task UpdateAsync(CategoryAddDto cname, int id)
    {
        var GetCat = await _CategoryRepo.Get(id);
        if (GetCat == null)
        {
            throw new KeyNotFoundException("Invalid id");
        }
        GetCat.Name = cname.Name;

        await _CategoryRepo.Update(id, GetCat);
    }

     public async Task DeleteAsync(int id)
    {
        var check = await _CategoryRepo.Get(id);
        if (check == null)
        {
            throw new KeyNotFoundException("invalid id");
        }
        await _CategoryRepo.Delete(id);

    }
   

   

  

    
}
