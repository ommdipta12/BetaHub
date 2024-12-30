using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IdentityServer.Helpers
{
	public static class IdentityHelper
	{
		public static void AddIdentityServerConfig(this IServiceCollection services, IConfiguration configuration)
		{
			// Load client configuration directly from appsettings.json
			var clients = configuration.GetSection("IdentityServer:Clients").Get<List<Client>>();

			services.AddIdentityServer()
				.AddInMemoryClients(clients!)
				.AddInMemoryIdentityResources(new List<IdentityResource>
				{
					new IdentityResources.OpenId(),
					new IdentityResources.Profile()
				})
				.AddInMemoryApiResources(new List<ApiResource>
				{
					new ApiResource("api1", "API 1")
				})
				.AddInMemoryApiScopes(new List<ApiScope>
				{
					new ApiScope("api1.read", "Read access to API 1"),
					new ApiScope("api1.write", "Write access to API 1")
				})
				.AddTestUsers(new List<TestUser>
				{
					new TestUser
					{
						SubjectId = "1",
						Username = "alice",
						Password = "password",
						Claims = new List<Claim>
						{
							new Claim("name", "Alice Smith"),
							new Claim("email", "alice@example.com"),
							new Claim("role", "admin")
						}
					},
					new TestUser
					{
						SubjectId = "2",
						Username = "bob",
						Password = "password",
						Claims = new List<Claim>
						{
							new Claim("name", "Bob Johnson"),
							new Claim("email", "bob@example.com"),
							new Claim("role", "user")
						}
					}
				})
				.AddDeveloperSigningCredential();
		}
	}
}
