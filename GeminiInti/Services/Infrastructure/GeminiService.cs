using GeminiInti.Model;
using GeminiInti.Services.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GeminiInti.Services.Infrastructure
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> GetGeminiResponse(string prompt)
        {
            string jsonstring = JsonConvert.SerializeObject(new GeminiContents()
            {
                contents = new List<GeminiParts>() {
                    new GeminiParts()
                    {
                        parts = new List<GeminiTexts>() {
                            new GeminiTexts() { text = prompt }
                        }
                    }
                }
            }, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }).ToString();
            var content = new StringContent(jsonstring, null, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_configuration.GetValue<string>("Gemini:ApiKey")}"))
            {
                Content = content
            };
            return await _httpClient.SendAsync(request);
        }
    }
}
