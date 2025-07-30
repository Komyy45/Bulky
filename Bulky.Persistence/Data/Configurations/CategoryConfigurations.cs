using Bulky.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bulky.Persistence.Data.Configurations;

public class CategoryConfigurations : BaseEntityConfigurations<Category, int>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}