using System.Reflection;
using System.Reflection.Emit;
using Bulky.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Persistence.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Id = Guid.Parse("a3e18d92-5c8b-4e06-9b9b-1d1f8038b0ef").ToString(), Name = "Admin", NormalizedName = "ADMIN" },
				new IdentityRole { Id = Guid.Parse("7f3f1b8c-34aa-48e9-bc0e-b0d5f0149c0b").ToString(), Name = "Company", NormalizedName = "COMPANY" },
				new IdentityRole { Id = Guid.Parse("c928dc5f-6b5c-4c33-a52b-2fa1f78fc9d2").ToString(), Name = "Employee", NormalizedName = "EMPLOYEE" },
				new IdentityRole { Id = Guid.Parse("0b0de5a6-06b4-4b3b-b913-17c9b3ea4f2f").ToString(), Name = "Customer", NormalizedName = "CUSTOMER" }
			);
		}

		public DbSet<Category> Categories { get; set; } 
		public DbSet<Product> Products { get; set; } 
	}
}
