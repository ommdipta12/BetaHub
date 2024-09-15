using BetaHub.Auth.Model.Options;
using Microsoft.Extensions.Options;

namespace BetaHub.Auth.Service.Infrastructure.Context
{
	public class DatabaseOptionSetup : IConfigureOptions<DatabaseOptions>
	{
		private readonly IConfiguration _configuration;
		public DatabaseOptionSetup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Configure(DatabaseOptions options)
		{
			options.ConnectionString = _configuration.GetConnectionString("Database")!;
			_configuration.GetSection("DatabaseOptions").Bind(options);
		}
	}
}
