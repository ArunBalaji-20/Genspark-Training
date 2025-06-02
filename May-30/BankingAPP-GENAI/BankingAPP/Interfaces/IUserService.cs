using System;
using BankingAPP.DTOs;
using BankingAPP.Models;

namespace BankingAPP.Interfaces;

public interface IUserService
{
    public Task<User> AddUser(UserAddRequestDto user);

    Task<IEnumerable<User>> GetAllUsers();
   
}
