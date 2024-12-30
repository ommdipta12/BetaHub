using Hangfire;
using Hangfire.SqlServer;

namespace BetaHub.Auth.Helpers
{
	public static class HangfireHelper
	{
		public static void AddHangfireConfig(this IServiceCollection services, IConfiguration configuration)
		{
			//Hangfire
			services.AddHangfire(config => config
						.UseSimpleAssemblyNameTypeSerializer()
						.UseRecommendedSerializerSettings()
						.UseSqlServerStorage(configuration.GetConnectionString("HangfireDatabase"),
						new SqlServerStorageOptions
						{
							CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
							SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
							QueuePollInterval = TimeSpan.Zero,
							UseRecommendedIsolationLevel = true,
							DisableGlobalLocks = true
						}));
			services.AddHangfireServer();
		}
	}
}
