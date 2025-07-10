using System;
using BankingAPP.Contexts;
using BankingAPP.Interfaces;
using BankingAPP.Models;
using BankingAPP.Models.DTOs;

namespace BankingAPP.Services;

public class TransactionServices : ITransactionService
{
    private readonly IRepository<int, User> _userRepository;
    private readonly IRepository<int, Transaction> _transactionRepository;
    private readonly BankContext _bankContext;

    public TransactionServices(IRepository<int, Transaction> transactionRepository,
                               IRepository<int, User> userRepository,
                               BankContext bankContext)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _bankContext = bankContext;
    }
    public async Task<Transaction> Deposit(TransactionRequestDto request)
    {
        var user = (await _userRepository.GetAll())
                    .FirstOrDefault(u => u.AccountNumber == request.AccountNumber);

        if (user == null)
        {
            throw new Exception("Invalid Accountnumber");
        }
        user.Balance += request.Amount;

        var transaction = new Transaction
        {
            AccountNumber = request.AccountNumber,
            Amount = request.Amount,
            Date = DateTime.UtcNow,
            Type = "Deposit"
        };

        await _transactionRepository.Add(transaction);
        await _userRepository.Update(user);

        return transaction;

    }



    public async Task<Transaction> Withdraw(TransactionRequestDto request)
    {
        var user = (await _userRepository.GetAll())
                    .FirstOrDefault(u => u.AccountNumber == request.AccountNumber);

        if (user == null)
        {
            throw new Exception("Invalid Accountnumber");
        }
        if (user.Balance < request.Amount)
            throw new Exception("Insufficient balance");
        user.Balance -= request.Amount;

        var transaction = new Transaction
        {
            AccountNumber = request.AccountNumber,
            Amount = request.Amount,
            Date = DateTime.UtcNow,
            Type = "Withdraw"
        };

        await _transactionRepository.Add(transaction);
        await _userRepository.Update(user);

        return transaction;
    }


    public async Task<Transaction> Transfer(TransferRequestDto request)
    {
        using var DbTransaction = await _bankContext.Database.BeginTransactionAsync();

        try
        {
            var users = await _userRepository.GetAll();
            var sourceUser = users.FirstOrDefault(s => s.AccountNumber == request.SourceAccountNumber);
            var targetUser = users.FirstOrDefault(t => t.AccountNumber == request.TargetAccountNumber);

            if (sourceUser == null)
            {
                throw new Exception("Source user not found");
            }
            if (targetUser == null)
            {
                throw new Exception("Target user not found");
            }

            if (sourceUser.Balance < request.Amount)
            {
                throw new Exception("Insufficient balance in source account");
            }

            sourceUser.Balance -= request.Amount;
            targetUser.Balance += request.Amount;

            await _userRepository.Update(sourceUser);
            await _userRepository.Update(targetUser);

            var transactionRec = new Transaction
            {
                Type = "Transfer",
                Amount = request.Amount,
                Date = DateTime.UtcNow,
                TargetAccountNumber = request.TargetAccountNumber,
                AccountNumber = request.SourceAccountNumber
            };
            await _transactionRepository.Add(transactionRec);

            await DbTransaction.CommitAsync();

            return transactionRec;
        }
        catch (Exception e)
        {
            await DbTransaction.RollbackAsync();
            throw;
        }
    }
}
