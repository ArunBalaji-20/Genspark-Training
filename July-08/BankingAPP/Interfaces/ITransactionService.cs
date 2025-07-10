using System;
using BankingAPP.Models;
using BankingAPP.Models.DTOs;

namespace BankingAPP.Interfaces;

public interface ITransactionService
{
    Task<Transaction> Deposit(TransactionRequestDto request);
    Task<Transaction> Withdraw(TransactionRequestDto request);
    Task<Transaction> Transfer(TransferRequestDto request);
}