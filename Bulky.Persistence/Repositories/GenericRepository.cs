using System.Linq.Expressions;
using Bulky.Core.Contracts.Ports.Repositories;
using Bulky.Core.Contracts.Specification;
using Bulky.Core.Entities.common;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Persistence.Repositories;

public class GenericRepository<TEntity, TKey>(DbContext dbContext) : IGenericRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    
    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = _dbSet.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = _dbSet.Evaluate(spec);
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> Get(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }
    
    public async Task<TEntity?> Get(int id, ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken)
    {
        return await _dbSet.Evaluate(spec).FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task Add(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<int> Delete(int id, CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(e => e.Id.Equals(id));
        return await query.ExecuteDeleteAsync(cancellationToken);
    }

	public async Task<int> CountAsync(CancellationToken cancellationToken)
	{
        return await _dbSet.CountAsync(cancellationToken);
	}

	public async Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
	{
		return await _dbSet.CountAsync(criteria, cancellationToken);
	}
}