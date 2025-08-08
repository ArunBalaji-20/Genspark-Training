using System;
using ChienShopMigrationProject.Dtos;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Interfaces;

public interface IColorService
{
    Task<Color> GetByIdAsync(int id);
    Task AddColor(ColorAddDto colorAddDto);

    Task DeleteAsync(int id);

    Task UpdateAsync(ColorAddDto dto, int id);

    Task<IEnumerable<Color>> GetAll();

}
