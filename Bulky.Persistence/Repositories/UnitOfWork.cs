using System.Collections.Concurrent;
using Bulky.Core.Contracts.Ports.Repositories;
using Bulky.Core.Entities.common;
using Bulky.Persistence.Data;

namespace Bulky.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    
    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name,
            new GenericRepository<TEntity, TKey>(dbContext));
    }
    
    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
    }

    public async ValueTask<int> CompleteAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}