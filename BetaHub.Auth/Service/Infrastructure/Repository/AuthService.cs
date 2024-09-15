using BetaHub.Auth.Model.Entities.Auth;
using BetaHub.Auth.Model.Entities.Auth.Params;
using BetaHub.Auth.Model.Entities.Auth.Responses;
using BetaHub.Auth.Service.Application.Interface;
using BetaHub.Auth.Service.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BetaHub.Auth.Service.Infrastructure.Repository
{
	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _dbContext;

		public AuthService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<UserVerifyResponse> getUserVerify(UserVerifyParam param)
		{
			var User = await _dbContext.Set<UserRegistration>()
					.FirstOrDefaultAsync(o => (o.Mobile == param.UserPass || o.Email == param.UserPass)
										&& param.Captcha == "ABCD"
										&& (o.LockOutEnable == false || o.UnlockTime < DateTime.Now));

			if (User != null)
			{
				// Return a new object if condition is satisfied
				return new UserVerifyResponse
				{
					Email = User.Email,
					Mobile = User.Mobile,
					LoginType = User.LoginType
				};
			}
			else
			{
				// Return a different object if no user is found or condition fails
				return null;
			}
		}
	}
}
