using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Contracts.Ports.EmailService;
using Microsoft.Extensions.DependencyInjection;

namespace Bulky.Email.Adapter
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMailServices(this IServiceCollection services)
		{
			services.AddScoped<IEmailService, EmailService>();

			return services;
		}
	}
}
