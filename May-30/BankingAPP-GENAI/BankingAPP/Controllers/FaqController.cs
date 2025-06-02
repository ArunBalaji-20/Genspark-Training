using Microsoft.AspNetCore.Mvc;
using BankApp.Models;
using System.Threading.Tasks;
using BankApp.Interfaces;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly IGenAIService _genAIService;

        public FaqController(IGenAIService genAIService)
        {
            _genAIService = genAIService;
        }

        [HttpPost("Gemini/ask")]
        public async Task<IActionResult> AskGemini([FromBody] FAQRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("Prompt is required.");

            var result = await _genAIService.GetFAQResponseFromGeminiAsync(request.Prompt);
            return Ok(new { answer = result });
        }

         [HttpPost("Phi2/ask")]
        public async Task<IActionResult> AskPhi2([FromBody] FAQRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("Prompt is required.");

            var result = await _genAIService.GetFAQResponseFromPhiAsync(request.Prompt);
            return Ok(new { answer = result });
        }


    }
}
