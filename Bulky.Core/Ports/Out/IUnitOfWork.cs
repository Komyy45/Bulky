using Bulky.Core.Domain.Entities.common;

namespace Bulky.Core.Ports.Out;

public interface IUnitOfWork : IAsyncDisposable
{
	public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>;

	public ValueTask<int> CompleteAsync();
}