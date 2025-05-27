using System;
using System.ComponentModel.DataAnnotations;

namespace TwitterAPI.Models;

public class Users
{
//     Users
// UserId -pk |FirstName | LastName |UserName | passwordHash | Phone | email 
    [Key]
    public required string UserId { get; set; }

    public required string FirstName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public required string Username { get; set; }

    public required string PasswordHash { get; set; }

    public string email { get; set; } = string.Empty;

    public int phone { get; set; }


}
