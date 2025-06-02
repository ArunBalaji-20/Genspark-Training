using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Interfaces;

namespace BankApp.Services
{
    public class GenAIService : IGenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey ;

        public GenAIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(5);

             _apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY")
                 ?? throw new Exception("GEMINI_API_KEY is not set.");
        }
        public async Task<string> GetFAQResponseFromGeminiAsync(string userPrompt)
        {
            string prePrompt = "You are a helpful banking application FAQ assistant. Always respond clearly in 3 to 5 bullet points only.";
            string fullPrompt = $"{prePrompt}\n\nUser question: {userPrompt}";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = fullPrompt }
                        }
                    }
                }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}")
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

// #pragma warning disable CS8603 // Possible null reference return.
            return doc.RootElement
                      .GetProperty("candidates")[0]
                      .GetProperty("content")
                      .GetProperty("parts")[0]
                      .GetProperty("text")
                      .GetString();
// #pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<string> GetFAQResponseFromPhiAsync(string question)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"http://127.0.0.1:9001/generate")
            {
                    Content = new StringContent(
                    JsonSerializer.Serialize(new { prompt = question }), 
                    Encoding.UTF8, 
                    "application/json"
                    )

            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            return doc.RootElement.GetProperty("generated_text").GetString();

        }
    }
}
