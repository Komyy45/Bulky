using Bulky.Core.Entities.common;

namespace Bulky.Core.Contracts;

public interface IGenericRepository<TEntity, TKey>
where TEntity : BaseEntity<TKey> 
where TKey : IEquatable<TKey>
{
    public Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken, bool asNoTracking = false);
    public Task<TEntity?> Get(int id, CancellationToken cancellationToken);
    public Task Add(TEntity entity, CancellationToken cancellationToken);
    public void Update(TEntity entity);
    public Task<int> Delete(int id, CancellationToken cancellationToken);
}