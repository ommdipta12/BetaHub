namespace GeminiInti.Services.Application
{
    public interface IGeminiService
    {
        Task<HttpResponseMessage> GetGeminiResponse(string prompt);
    }
}
