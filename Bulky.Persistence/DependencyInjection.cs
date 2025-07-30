using Bulky.Core.Ports.Out;
using Bulky.Persistence.Data;
using Bulky.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bulky.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(cfg =>
				cfg.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			
			services.AddTransient<DbContextInitializer>();
			
			return services;
		}
	}
}
