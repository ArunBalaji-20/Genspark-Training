using System;

namespace BankingAPP.Models;

public class User
{
    public int AccountNumber { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public decimal Balance { get; set; }
    
    public ICollection<Transaction>? transactions{ get; set; }
}
