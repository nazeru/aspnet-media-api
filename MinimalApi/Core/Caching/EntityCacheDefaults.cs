using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Caching;

public static class EntityCacheDefaults<TEntity> where TEntity : BaseEntity
{
    public static string EntityTypeName => typeof(TEntity).Name.ToLowerInvariant();

    public static CacheKey ByIdCacheKey => new($"cache.{EntityTypeName}.byid.{{0}}", ByIdPrefix, Prefix);

    public static CacheKey ByIdsCacheKey => new($"cache.{EntityTypeName}.byids.{{0}}", ByIdsPrefix, Prefix);

    public static CacheKey AllCacheKey => new($"cache.{EntityTypeName}.all.", AllPrefix, Prefix);

    public static string Prefix => $"cache.{EntityTypeName}.";

    public static string ByIdPrefix => $"cache.{EntityTypeName}.byid.";

    public static string ByIdsPrefix => $"cache.{EntityTypeName}.byids.";

    public static string AllPrefix => $"cache.{EntityTypeName}.all.";
}