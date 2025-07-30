using Bulky.Core.Domain.Entities.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bulky.Persistence.Data.Configurations;

public class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
    }
}