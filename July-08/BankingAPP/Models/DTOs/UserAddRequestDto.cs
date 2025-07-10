namespace BankingAPP.DTOs
{
    public class UserAddRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal InitialDeposit { get; set; }
    }
}
