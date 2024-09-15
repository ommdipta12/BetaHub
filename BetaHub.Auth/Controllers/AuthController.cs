using BetaHub.Auth.Model.Entities.Auth.Params;
using BetaHub.Auth.Service.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BetaHub.Auth.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		public async Task<JsonResult> getVerifyUser([FromBody]UserVerifyParam param)
		{
			var result = await _authService.getUserVerify(param);
			if (result != null)
			{
				return new JsonResult(new { success = true, result })
				{
					StatusCode = StatusCodes.Status200OK
				};
			}
			else
			{
				return new JsonResult(new { success = false })
				{
					StatusCode = StatusCodes.Status404NotFound
				};
			}
		}
	}
}
