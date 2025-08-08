using System;
using System.Threading.Tasks;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Dtos;
using ChienVHShopOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Services;

public class ColorService : IColorService
{
   
    private readonly IRepository<int, Color> _ColorRepo;

    public ColorService(IRepository<int, Color> ColorRepo)
    {
        _ColorRepo = ColorRepo;

       
    }

    public async Task<IEnumerable<Color>> GetAll()
    {
        return await _ColorRepo.GetAll();
    }


    public async Task<Color> GetByIdAsync(int id)
    {
        return await _ColorRepo.Get(id);
    }



    public async Task AddColor(ColorAddDto colorAddDto)
    {
        var checkExist = await checkColorExist(colorAddDto.Color1);
        if (checkExist)
        {
            throw new Exception("Color already present");
        }
        else
        {
            var newColor = new Color
            {
                Color1 = colorAddDto.Color1
            };
            await _ColorRepo.Add(newColor);
        }
        
    }

    private async Task<bool> checkColorExist(string color)
    {
        var all = await _ColorRepo.GetAll();
        bool result = all.Any(e => e.Color1 == color);
        return result;
    }

    public async Task UpdateAsync(ColorAddDto dto, int id)
    {
        var GetColor = await _ColorRepo.Get(id);
        if (GetColor == null)
        {
            throw new KeyNotFoundException("Invalid id");
        }
        GetColor.Color1 = dto.Color1;
        
        await _ColorRepo.Update(id, GetColor);
    }

    public async Task DeleteAsync(int id)
    {
        var check = await _ColorRepo.Get(id);
        if (check == null)
        {
            throw new KeyNotFoundException("invalid id");
        }
        await _ColorRepo.Delete(id);

    }

    
}
