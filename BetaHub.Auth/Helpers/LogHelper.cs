using Serilog;

namespace BetaHub.Auth.Helpers
{
	public static class LogHelper
	{
		public static void AddLogConfig(this IHostBuilder hostBuilder)
		{
			// Configure the log to be written to the wwwroot/logs folder
			hostBuilder.UseSerilog((context, services, configuration) =>
			{
				// Build Serilog configuration
				configuration
					.ReadFrom.Configuration(context.Configuration)  // Read config from appsettings.json if needed
					.Enrich.FromLogContext(); // Enrich logs with contextual information
			});
		}
	}
}
