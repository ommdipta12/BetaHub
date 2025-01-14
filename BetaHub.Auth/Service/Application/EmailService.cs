using BetaHub.Auth.Model.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace BetaHub.Auth.Service.Application
{
	public class EmailService : IEmailSender
	{
		private MailOptions _mailOptions;

		public EmailService(IOptions<MailOptions> mailOptions)
		{
			_mailOptions = mailOptions.Value;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			SmtpClient client = new SmtpClient
			{
				Port = _mailOptions.Port,
				Host = _mailOptions.Host,
				EnableSsl = _mailOptions.UseSSL,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = _mailOptions.DefaultCredentials,
				Credentials = new NetworkCredential(_mailOptions.UserName, _mailOptions.Password)
			};

			MailMessage message = new MailMessage(_mailOptions.EmailId, email, subject, htmlMessage);
			message.IsBodyHtml = true;

			await client.SendMailAsync(message);
		}
	}
}
