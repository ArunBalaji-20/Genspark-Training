using System;

namespace BankingAPP.Models.DTOs;

public class TransactionRequestDto
{
    public int AccountNumber { get; set; }
    public decimal Amount { get; set; }
}
