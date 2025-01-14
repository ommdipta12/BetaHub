using GeminiInti.Services.Infrastructure;

namespace GeminiInti.Services.Application
{
    public static class InfraService
    {
        public static void AddGeminiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGeminiService, GeminiService>();
        }
    }
}
