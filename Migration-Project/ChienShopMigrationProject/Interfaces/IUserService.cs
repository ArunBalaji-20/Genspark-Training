using System;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;

namespace ChienShopMigrationProject.Interfaces;

public interface IUserService
{
    // public Task<User> Register(RegisterUserDto user);
    Task<UserLoginResponse> Register(RegisterUserDto user);
    Task<UserLoginResponse> Login(RegisterUserDto user);
}
