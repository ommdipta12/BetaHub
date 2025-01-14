using GeminiInti.Model;
using GeminiInti.Services.Application;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GeminiInti.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeminiGenController : ControllerBase
    {
        private readonly IGeminiService _geminiService;

        public GeminiGenController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswer(string prompt)
        {
            var response = await _geminiService.GetGeminiResponse(prompt);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<GeminiResponse>(jsonResponse);
            return Ok(res);
        }
    }
}
