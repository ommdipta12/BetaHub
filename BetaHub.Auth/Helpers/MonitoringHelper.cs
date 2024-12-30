using OpenTelemetry.Metrics;

namespace BetaHub.Auth.Helpers
{
	public static class MonitoringHelper
	{
		public static void AddMoniotringConfig(this IServiceCollection services, IConfiguration configuration)
		{
			//Health check
			services.AddHealthChecks()
				.AddSqlServer(configuration.GetConnectionString("Database")!, tags: ["sql-server"]);
			services.AddHealthChecksUI(opt =>
			{
				//opt.AddHealthCheckEndpoint("sql-server", "/api/_healthz");
				//opt.MaximumHistoryEntriesPerEndpoint(50);
			}).AddInMemoryStorage();

			//OpenTelemetry
			services.AddOpenTelemetry()
				.WithMetrics(o =>
				{
					o.AddPrometheusExporter();
					o.AddMeter(
						"Microsoft.AspNetCore.Hosting",
						"Microsoft.AspNetCore.Server.Kestrel");

					o.AddAspNetCoreInstrumentation()
					.AddHttpClientInstrumentation()
					.AddRuntimeInstrumentation()
					.AddPrometheusExporter();

				});
		}
	}
}
