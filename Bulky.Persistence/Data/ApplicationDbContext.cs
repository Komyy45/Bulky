using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Persistence.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<Category> Categories { get; set; } 
		public DbSet<Product> Products { get; set; } 
	}
}
