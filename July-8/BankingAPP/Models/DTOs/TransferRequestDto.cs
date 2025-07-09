using System;

namespace BankingAPP.Models.DTOs;


public class TransferRequestDto
{
    public int SourceAccountNumber { get; set; }
    public int TargetAccountNumber { get; set; }
    public decimal Amount { get; set; }
}

