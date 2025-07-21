namespace Bulky.Core.Entities.common;

public abstract class BaseEntity<TKey>
where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}