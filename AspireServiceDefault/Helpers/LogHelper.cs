using Microsoft.Extensions.Hosting;
using Serilog;

namespace Aspire.AspireServiceDefault.Helpers
{
	public static class LogHelper
	{
		public static void ConfigSeriLog(this IHostBuilder hostBuilder)
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
