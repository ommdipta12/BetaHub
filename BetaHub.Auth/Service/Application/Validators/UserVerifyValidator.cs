using BetaHub.Auth.Model.Entities.Auth.Params;
using FluentValidation;

namespace BetaHub.Auth.Service.Application.Validators
{
	public class UserVerifyValidator : AbstractValidator<UserVerifyParam>
	{
		public UserVerifyValidator()
		{
			RuleFor(o => o.UserPass)
				.NotEmpty()
				.WithMessage("{PropertyName} can't be empty!");
			RuleFor(o => o.Captcha)
				.NotEmpty()
				.WithMessage("{PropertyName} can't be empty!")
				.Length(4)
				.WithMessage("{PropertyName} must 4 character!");
		}
	}
}
