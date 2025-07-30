using System.Linq.Expressions;
using System.Threading;
using Bulky.Core.Domain.Entities.common;
using Bulky.Core.Domain.Specification;
using Bulky.Core.Domain.Entities;

namespace Bulky.Core.Ports.Out;

public interface IGenericRepository<TEntity, TKey>
where TEntity : BaseEntity<TKey>
where TKey : IEquatable<TKey>
{
	public Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken, bool asNoTracking = false);
	public Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken, bool asNoTracking = false);
	public Task<TEntity?> Get(int id, CancellationToken cancellationToken);
	public Task<TEntity?> Get(int id, ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken);
	public Task<int> CountAsync(CancellationToken cancellationToken);
	public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
	public Task Add(TEntity entity, CancellationToken cancellationToken);
	public void Update(TEntity entity);
	public Task<int> Delete(int id, CancellationToken cancellationToken);
}