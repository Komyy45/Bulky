using Bulky.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bulky.Persistence.Data.Configurations;

public class ProductConfigurations : BaseEntityConfigurations<Product, int>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title).HasMaxLength(100).IsRequired();

        builder.Property(e => e.Description).HasMaxLength(300).IsRequired();

        builder.Property(e => e.ISBN).IsRequired();

        builder.Property(e => e.Price).IsRequired();

        builder.Property(e => e.Author).HasMaxLength(100).IsRequired();

        builder.HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryId);
    }
}