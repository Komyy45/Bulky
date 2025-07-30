using Bulky.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bulky.Persistence.Data.Configurations
{
	public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

			builder.HasOne(u => u.Address).WithOne().HasForeignKey<Address>(a => a.UserId);
		}
	}
}
