using System;
using BankingAPP.Contexts;
using BankingAPP.Interfaces;
using BankingAPP.Models;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace BankingAPP.Repositories;

public class UserRepository : Repository<int, User>
{
    public UserRepository(BankContext bankContext) : base(bankContext)
    {
    }

    public override Task<User> Get(int key)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
         var allUsers = await _bankContext.Users
                        .Include(u=>u.transactions)
                        .ToListAsync();
         return allUsers;
    }
}
