using Bulky.Core.Ports.Out;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Bulky.Basket.Adapter
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddBasketServices(IServiceCollection services, IConfiguration configuration) 
		{
			
			return services;
		}
	}
}
