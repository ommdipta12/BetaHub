using BetaHub.Auth.Model.Options;
using BetaHub.Auth.Service.Infrastructure.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BetaHub.Auth.Service.Application
{
	public static class InfraServices
	{
		public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
		{
			//Database Connection or Dbcontext setting
			services.ConfigureOptions<DatabaseOptionSetup>();
			services.AddDbContext<ApplicationDbContext>(
				(serviceProvider, dbContextProvider) =>
				{
					var dbsetup = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

					dbContextProvider.UseSqlServer(dbsetup.ConnectionString, sqlServerAction =>
					{
						sqlServerAction.EnableRetryOnFailure(dbsetup.MaxRetryCount);
						sqlServerAction.CommandTimeout(dbsetup.CommandTimeout);
					});

					dbContextProvider.EnableDetailedErrors(dbsetup.EnableDetailedErrors);
					dbContextProvider.EnableSensitiveDataLogging(dbsetup.EnableSensitiveDataLogging);
				}
			);

			//Identity Services
			services.AddAuthorization();
			services.AddIdentityApiEndpoints<IdentityUser>(opt =>
			{
				opt.Password.RequiredLength = 4;
				opt.User.RequireUniqueEmail = true;
				opt.Password.RequireNonAlphanumeric = false;
				opt.SignIn.RequireConfirmedEmail = false;
			}).AddEntityFrameworkStores<ApplicationDbContext>();
			services.ConfigureOptions<MailOptionSetup>();
			services.AddTransient<IEmailSender, EmailService>();

			//Fluent validation
			services.AddFluentValidationAutoValidation(options =>
			{
				options.DisableDataAnnotationsValidation = true;
			});
		}
	}
}
