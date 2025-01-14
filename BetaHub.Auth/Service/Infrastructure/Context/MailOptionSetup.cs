using BetaHub.Auth.Model.Options;
using Microsoft.Extensions.Options;

namespace BetaHub.Auth.Service.Infrastructure.Context
{
	public class MailOptionSetup : IConfigureOptions<MailOptions>
	{
		private readonly IConfiguration _configuration;
		public MailOptionSetup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Configure(MailOptions options)
		{
			_configuration.GetSection("MailSettings").Bind(options);
		}
	}
}
