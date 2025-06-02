using System;
using System.ComponentModel.DataAnnotations;

namespace BankingAPP.Models;

public class Transaction
{
    public int TransactionId { get; set; }

    public string Type { get; set; } = string.Empty; // "Deposit", "Withdraw", "Transfer"

    public decimal Amount { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int? TargetAccountNumber { get; set; }

    // Foreign key
    public int AccountNumber { get; set; }

    public User? User { get; set; }
}