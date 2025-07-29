using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Models.Email;

namespace Bulky.Core.Contracts.Ports.EmailService
{
	public interface IEmailService
	{
		public Task SendEmail(EmailMessage message);
	}
}
