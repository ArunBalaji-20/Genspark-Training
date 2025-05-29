using System;
using BankingAPP.Contexts;
using BankingAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPP.Repositories;

public class TransactionRepository : Repository<int, Transaction>
{
    public TransactionRepository(BankContext bankContext) : base(bankContext)
    {
    }
    public override Task<Transaction> Get(int key)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<Transaction>> GetAll()
    {
        var Alltransactions = await _bankContext.Transactions.ToListAsync();
        return Alltransactions;
    }

         public async Task<IEnumerable<Transaction>> GetTransactionsByAccount(int accountNumber)
        {
            return await _bankContext.Transactions
                .Where(t => t.AccountNumber == accountNumber)
                .ToListAsync();
        }
}
