using BetaHub.Auth.Model.Options;
using BetaHub.Auth.Service.Application.Interface;
using BetaHub.Auth.Service.Application.Validators;
using BetaHub.Auth.Service.Infrastructure.Context;
using BetaHub.Auth.Service.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
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

			//Fluent validation
			services.AddFluentValidationAutoValidation(options =>
			{
				options.DisableDataAnnotationsValidation = true;
			});
			services.AddValidatorsFromAssemblyContaining<UserVerifyValidator>();

			//Service Register
			services.AddScoped<IAuthService, AuthService>();
		}
	}
}
