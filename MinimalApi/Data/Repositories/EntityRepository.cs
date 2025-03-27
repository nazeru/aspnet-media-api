using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Data.Repositories;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
{
    protected readonly IDatabaseFactory DatabaseFactory;

    protected DbContext Context => this.DatabaseFactory.Get();

    protected DbSet<TEntity> Entities => this.Context.Set<TEntity>();

    public EntityRepository(IDatabaseFactory databaseFactory) => this.DatabaseFactory = databaseFactory;

    public virtual IQueryable<TEntity> GetEntities() => (IQueryable<TEntity>) this.Entities;

    public virtual async Task<TEntity> GetByKeyAsync(params object[] keys) => await this.Entities.FindAsync(keys);

    public virtual async Task<TEntity> GetByKeyAsync(
      object[] keys,
      CancellationToken cancellationToken)
    {
        return await this.Entities.FindAsync(keys, cancellationToken);
    }

    public virtual void Add(TEntity entity) => this.Entities.Add(entity);

    public virtual void AddRange(IEnumerable<TEntity> entities) => Entities.AddRange(entities);

    public virtual void Update(TEntity entity)
    {
        if (this.Context.Entry<TEntity>(entity).State != EntityState.Detached)
            return;
        this.Entities.Attach(entity);
        this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        if (this.Context.Entry<TEntity>(entity).State != EntityState.Detached)
        {
            this.Entities.Remove(entity);
        }
        else
        {
            this.Entities.Attach(entity);
            this.Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }
    }

    public void DeleteRange(IEnumerable<TEntity> entities) => Entities.RemoveRange(entities);

    public Task<List<TEntity>> ToListAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return (Task<List<TEntity>>) EntityFrameworkQueryableExtensions.ToListAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }

    public Task<TEntity> FirstOrDefaultAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return (Task<TEntity>) EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }

    public Task<TEntity> FirstAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return (Task<TEntity>) EntityFrameworkQueryableExtensions.FirstAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }

    public Task<TEntity> SingleOrDefaultAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return (Task<TEntity>) EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }

    public Task<TEntity> SingleAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return (Task<TEntity>) EntityFrameworkQueryableExtensions.SingleAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }

    public Task<bool> AnyAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return EntityFrameworkQueryableExtensions.AnyAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }

    public Task<int> CountAsync(
      IQueryable<TEntity> queryable,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return EntityFrameworkQueryableExtensions.CountAsync<TEntity>((IQueryable<TEntity>) queryable, cancellationToken);
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) => 
        Context.SaveChangesAsync();
}
    