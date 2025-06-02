namespace BankApp.Interfaces
{
    public interface IGenAIService
    {
        Task<string> GetFAQResponseFromGeminiAsync(string question);

        Task<String> GetFAQResponseFromPhiAsync(string question);
    }
}
