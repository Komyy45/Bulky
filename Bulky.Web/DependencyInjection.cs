using Bulky.BlobService;
using Bulky.Core;
using Bulky.Core.Contracts.Ports.UrlService;
using Bulky.Core.Entities;
using Bulky.Email.Adapter;
using Bulky.Persistence;
using Bulky.Persistence.Data;
using Bulky.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

			services.AddSingleton<IConfiguration>(c => configuration);

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.AddScoped<IUrlService, UrlService>();


			return services;
		}
	}
}
