using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Application.Models.Email;

namespace Bulky.Core.Ports.Out
{
	public interface IEmailService
	{
		public Task SendEmail(EmailMessage message);
	}
}
