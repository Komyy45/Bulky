
using Bulky.BlobService;
using Bulky.Core;
using Bulky.Persistence;
using Bulky.Persistence.Data;

namespace Bulky.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddCoreServices()
                            .AddPersistenceServices(builder.Configuration)
                            .AddBlobStorageServices(builder.Configuration);

            builder.Services.AddSingleton<IConfiguration>(c => builder.Configuration);

            var app = builder.Build();

            var dbContextInitializer = app.Services.CreateScope().ServiceProvider.GetService<DbContextInitializer>();
            await dbContextInitializer!.SeedAsync();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
