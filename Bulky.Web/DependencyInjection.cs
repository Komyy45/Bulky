using Bulky.BlobService;
using Bulky.Core;
using Bulky.Core.Domain.Entities;
using Bulky.Core.Ports.Out;
using Bulky.Email.Adapter;
using Bulky.Persistence;
using Bulky.Persistence.Data;
using Bulky.Web.Services;
using Microsoft.AspNetCore.Identity;
using Bulky.Basket.Adapter;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StackExchange.Redis;

namespace Bulky.Web
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddControllersWithViews();
			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedEmail = true;

				options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
			}).AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LogoutPath = "/Identity/Account/Logout";
				options.LoginPath = "/Identity/Account/Login";
				options.AccessDeniedPath = "/Customer/Home/Error";
				options.Events.OnSigningOut = (context) => { context.Response.Redirect("/Identity/Account/SignIn"); return Task.CompletedTask; };
			});

			services.AddCoreServices()
							.AddPersistenceServices(configuration)
							.AddBlobStorageServices(configuration)
							.AddMailServices();

			services.AddSingleton<IConnectionMultiplexer>(serviceProvider => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


			services.AddSingleton<IConfiguration>(c => configuration);

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.AddScoped<IUrlService, UrlService>();


			return services;
		}
	}
}
