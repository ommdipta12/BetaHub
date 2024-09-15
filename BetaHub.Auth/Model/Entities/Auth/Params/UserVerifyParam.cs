namespace BetaHub.Auth.Model.Entities.Auth.Params
{
	public class UserVerifyParam
	{
		public string UserPass { get; set; }
		public string Captcha { get; set; }
		public string UserKey { get; set; }
	}
}
