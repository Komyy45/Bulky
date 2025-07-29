using Bulky.Core.Contracts.Ports.EmailService;
using Bulky.Core.Models.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Bulky.Email.Adapter
{
	internal sealed class EmailService(IConfiguration configuration) : IEmailService
	{

		public async Task SendEmail(EmailMessage message)
		{
			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress
									("Bulky BookStore",
									message.From
									 ));
			mimeMessage.To.Add(new MailboxAddress
									 (message.To,
									 message.To
									 ));
			mimeMessage.Subject = message.Subject;
			mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = message.Body
			};

			using (var client = new SmtpClient())
			{
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;

				client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
				client.Authenticate(
					configuration["MailKit:Email"],
					configuration["MailKit:Password"]
				);
				await client.SendAsync(mimeMessage);
				await client.DisconnectAsync(true);
			}
		}
	}
}
