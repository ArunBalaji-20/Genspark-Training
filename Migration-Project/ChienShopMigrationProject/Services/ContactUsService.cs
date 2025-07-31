using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Dtos;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Services;

public class ContactUsService:IContactUsService
{

    private readonly IRepository<int, ContactU> _contactus;

    public ContactUsService(IRepository<int, ContactU> contactus)
    {
        _contactus = contactus;


    }

    public async Task DeleteAsync(int id)
    {
        var checkDetails = await _contactus.Get(id);
        if (checkDetails == null)
        {
            throw new KeyNotFoundException("Invalid Key");
        }
        await _contactus.Delete(id);
    }

    public async Task<ContactU> GetByIdAsync(int id)
    {
        var result = await _contactus.Get(id);
        if (result == null)
        {
            throw new KeyNotFoundException("invalid id");
        }
        return result;
    }

    public async Task Submit(SubmitContactUsDto reqdto)
    {
        var newSubmission = new ContactU
        {
            name = reqdto.name,
            email = reqdto.email,
            content = reqdto.content,
            phone = reqdto.phone
        };
        await _contactus.Add(newSubmission);
    }

    public async Task UpdateAsync(SubmitContactUsDto reqdto, int id)
    {
        var checkDetails = await _contactus.Get(id);
        if (checkDetails == null)
        {
            throw new KeyNotFoundException("Invalid Key");
        }
        checkDetails.content = reqdto.content;
        checkDetails.email = reqdto.email;
        checkDetails.name = reqdto.name;
        checkDetails.phone = reqdto.phone;
        await _contactus.Update(id,checkDetails);
        
    }
}
