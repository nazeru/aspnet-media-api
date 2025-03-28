using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Caching;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;
using MinimalApi.Data.Extensions;
using MinimalApi.Shared;

namespace MinimalApi.Data.Repositories;

/// <inheritdoc/>
public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationContext _context;
    private readonly ICacheManager _staticCacheManager;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="staticCacheManager"></param>
    public EntityRepository(ApplicationContext context, ICacheManager staticCacheManager)
    {
        _context = context;
        _staticCacheManager = staticCacheManager;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(int id, Func<ICacheManager, CacheKey>? getCacheKey = null,
        bool includeDeleted = false)
    {
        async Task<TEntity?> getEntityAsync()
        {
            return await AddDeletedFilter(Query, includeDeleted).FirstOrDefaultAsync(entity => entity.Id == id);
        }

        if (getCacheKey == null)
        {
            return await getEntityAsync();
        }

        var cacheKey = getCacheKey(_staticCacheManager)
            ?? _staticCacheManager.PrepareKeyForDefaultCache(EntityCacheDefaults<TEntity>.ByIdCacheKey, id);

        return await _staticCacheManager.GetAsync(cacheKey, getEntityAsync);
    }

    /// <inheritdoc/>
    public virtual async Task<IList<TEntity>> GetByIdsAsync(IEnumerable<int> ids, Func<ICacheManager, CacheKey>? getCacheKey = null,
        bool includeDeleted = false)
    {
        if (ids.Count() == 0)
        {
            return new List<TEntity>();
        }

        async Task<IList<TEntity>?> getByIdsAsync()
        {
            var query = AddDeletedFilter(Query, includeDeleted);

            var entries = await query.Where(entry => ids.Contains(entry.Id)).ToListAsync();

            /*
                сортируем записи в порядке переданных идентификаторов
             */
            var sortedEntries = new List<TEntity>();
            foreach (var id in ids)
            {
                var sortedEntry = entries.Find(entry => entry.Id == id);
                if (sortedEntry != null)
                {
                    sortedEntries.Add(sortedEntry);
                }
            }

            return sortedEntries;
        }

        if (getCacheKey == null)
        {
            return await getByIdsAsync() ?? new List<TEntity>();
        }

        var cacheKey = getCacheKey(_staticCacheManager)
            ?? _staticCacheManager.PrepareKeyForDefaultCache(EntityCacheDefaults<TEntity>.ByIdsCacheKey, ids);

        return await _staticCacheManager.GetAsync(cacheKey, getByIdsAsync) ?? new List<TEntity>();
    }

    /// <inheritdoc/>
    public virtual IList<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null,
       Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false)
    {
        IList<TEntity> getAll()
        {
            var query = AddDeletedFilter(Query, includeDeleted);

            query = func != null ? func(query) : query;

            return query.ToList();
        }

        return GetEntities(getAll, getCacheKey) ?? new List<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<IList<TEntity>> GetAllAsync(Func<ICacheManager, CacheKey>? getCacheKey = null,
        bool includeDeleted = false)
    {
        async Task<IList<TEntity>?> getAllAsync()
        {
            var query = AddDeletedFilter(Query, includeDeleted);

            return await query.ToListAsync();
        }

        return await GetEntitiesAsync(getAllAsync, getCacheKey) ?? new List<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func,
        Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false)
    {
        async Task<IList<TEntity>?> getAllAsync()
        {
            var query = AddDeletedFilter(Query, includeDeleted);
            query = func(query);

            return await query.ToListAsync();
        }

        return await GetEntitiesAsync(getAllAsync, getCacheKey) ?? new List<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
        Func<ICacheManager, CacheKey>? getCacheKey = null, bool includeDeleted = false)
    {
        async Task<IList<TEntity>?> getAllAsync()
        {
            var query = AddDeletedFilter(Query, includeDeleted);
            query = await func(query);

            return await query.ToListAsync();
        }

        return await GetEntitiesAsync(getAllAsync, getCacheKey) ?? new List<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<PagedList<TEntity>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue,
        bool getOnlyTotalCount = false, bool includeDeleted = false)
    {
        var query = AddDeletedFilter(Query, includeDeleted);

        return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
    }

    /// <inheritdoc/>
    public virtual async Task<PagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func,
        int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = false)
    {
        var query = AddDeletedFilter(Query, includeDeleted);

        query = func != null ? func(query) : query;

        return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
    }

    /// <inheritdoc/>
    public virtual async Task<PagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func,
        int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = false)
    {
        var query = AddDeletedFilter(Query, includeDeleted);

        query = func != null ? await func(query) : query;

        return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
    }

    /// <inheritdoc/>
    public async Task<bool> AnyAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func, bool includeDeleted = false)
    {
        var query = AddDeletedFilter(Query, includeDeleted);
        query = func(query);

        return await query.AnyAsync();
    }

    /// <inheritdoc/>
    public async Task<int> CountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func, bool includeDeleted = false)
    {
        var query = AddDeletedFilter(Query, includeDeleted);
        query = func(query);

        return await query.CountAsync();
    }

    /// <inheritdoc/>
    public virtual async Task InsertAsync(TEntity entity, bool publishEvent = true)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _context.AddAsync(entity);

        // publish event here
    }

    /// <inheritdoc/>
    public virtual async Task InsertAsync(IList<TEntity> entities, bool publishEvent = true)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        await _context.AddRangeAsync(entities);

        // publish event here
    }

    /// <inheritdoc/>
    public virtual Task UpdateAsync(TEntity entity, bool publishEvent = true)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Update(entity);

        // publish event here

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task UpdateAsync(IList<TEntity> entities, bool publishEvent = true)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        if (entities.Count == 0)
        {
            return Task.CompletedTask;
        }

        _context.UpdateRange(entities);

        // publish event here

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task DeleteAsync(TEntity entity, bool publishEvent = true)
    {
        switch (entity)
        {
            case null:
                throw new ArgumentNullException(nameof(entity));
            case ISoftDeleteEntity softDeletedEntity:
                softDeletedEntity.Deleted = true;
                _context.Update(entity);
                break;
            default:
                _context.Remove(entity);
                break;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task DeleteAsync(IList<TEntity> entities, bool publishEvent = true)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        if (entities.OfType<ISoftDeleteEntity>().Any())
        {
            foreach (var entity in entities)
            {
                if (entity is ISoftDeleteEntity softDeletedEntity)
                {
                    softDeletedEntity.Deleted = true;
                    _context.Update(entity);
                }
            }
        }
        else
        {
            _context.RemoveRange(entities);
        }

        // publish event here

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> Query => _context.Set<TEntity>();

    protected virtual async Task<IList<TEntity>?> GetEntitiesAsync(Func<Task<IList<TEntity>?>> getAllAsync,
        Func<ICacheManager, CacheKey>? getCacheKey)
    {
        if (getCacheKey == null)
        {
            return await getAllAsync();
        }

        var cacheKey = getCacheKey(_staticCacheManager)
                       ?? _staticCacheManager.PrepareKeyForDefaultCache(EntityCacheDefaults<TEntity>.AllCacheKey);

        return await _staticCacheManager.GetAsync(cacheKey, getAllAsync);
    }

    protected virtual async Task<IList<TEntity>?> GetEntitiesAsync(Func<Task<IList<TEntity>?>> getAllAsync,
        Func<ICacheManager, Task<CacheKey>>? getCacheKey)
    {
        if (getCacheKey == null)
        {
            return await getAllAsync();
        }

        var cacheKey = await getCacheKey(_staticCacheManager)
                       ?? _staticCacheManager.PrepareKeyForDefaultCache(EntityCacheDefaults<TEntity>.AllCacheKey);

        return await _staticCacheManager.GetAsync(cacheKey, getAllAsync);
    }

    protected virtual IList<TEntity>? GetEntities(Func<IList<TEntity>?> getAll,
        Func<ICacheManager, CacheKey>? getCacheKey)
    {
        if (getCacheKey == null)
        {
            return getAll();
        }

        var cacheKey = getCacheKey(_staticCacheManager)
                       ?? _staticCacheManager.PrepareKeyForDefaultCache(EntityCacheDefaults<TEntity>.AllCacheKey);

        return _staticCacheManager.Get(cacheKey, getAll);
    }

    protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, bool includeDeleted)
    {
        if (includeDeleted)
        {
            return query;
        }

        if (typeof(TEntity).GetInterface(nameof(ISoftDeleteEntity)) == null)
        {
            return query;
        }

        return query.OfType<ISoftDeleteEntity>().Where(entry => !entry.Deleted).OfType<TEntity>();
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
    