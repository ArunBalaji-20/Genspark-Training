using System;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;

namespace ChienShopMigrationProject.Interfaces;

public interface IContactUsService
{
    Task<ContactU> GetByIdAsync(int id);

    Task Submit(SubmitContactUsDto reqdto);
    Task UpdateAsync( SubmitContactUsDto reqdto,int id);

    Task DeleteAsync(int id);
}
