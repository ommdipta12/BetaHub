using BetaHub.Auth.Model.Entities.Auth.Params;
using BetaHub.Auth.Model.Entities.Auth.Responses;

namespace BetaHub.Auth.Service.Application.Interface
{
	public interface IAuthService
	{
		Task<UserVerifyResponse> getUserVerify(UserVerifyParam param);
	}
}
