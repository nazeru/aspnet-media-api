namespace MinimalApi.Core.Repositories;

public interface IEntityRepository<TEntity>
{
    IQueryable<TEntity> GetEntities();

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);

    Task<List<TEntity>> ToListAsync(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<TEntity> FirstOrDefaultAsync(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<TEntity> FirstAsync(IQueryable<TEntity> queryable, CancellationToken cancellationToken = default(CancellationToken));

    Task<TEntity> SingleOrDefaultAsync(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<TEntity> SingleAsync(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> AnyAsync(IQueryable<TEntity> queryable, CancellationToken cancellationToken = default(CancellationToken));

    Task<int> CountAsync(IQueryable<TEntity> queryable, CancellationToken cancellationToken = default(CancellationToken));
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}