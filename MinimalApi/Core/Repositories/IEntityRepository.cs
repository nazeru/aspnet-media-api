using MinimalApi.Core.Caching;
using MinimalApi.Core.Entities;
using MinimalApi.Shared;

namespace MinimalApi.Core.Repositories;

/// <summary>
/// Репозиторий
/// </summary>
public partial interface IEntityRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id, Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

    Task<IList<TEntity>> GetByIdsAsync(IEnumerable<int> ids, Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

    IList<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null,
        Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

    Task<IList<TEntity>> GetAllAsync(Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

    Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func,
        Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

    Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
        Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false);

    Task<PagedList<TEntity>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue,
        bool getOnlyTotalCount = false, bool includeDeleted = false);

    Task<PagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func,
        int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = false);

    Task<PagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
        int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = false);

    Task<bool> AnyAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func, bool includeDeleted = false);

    Task<int> CountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func, bool includeDeleted = false);

    Task InsertAsync(TEntity entity, bool publishEvent = true);

    Task InsertAsync(IList<TEntity> entities, bool publishEvent = true);

    Task UpdateAsync(TEntity entity, bool publishEvent = true);

    Task UpdateAsync(IList<TEntity> entities, bool publishEvent = true);

    Task DeleteAsync(TEntity entity, bool publishEvent = true);

    Task DeleteAsync(IList<TEntity> entities, bool publishEvent = true);

    IQueryable<TEntity> Query { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}