namespace MinimalApi.Core.Caching;

public class NoopCacheManager : ICacheManager
{
    public Task ClearAsync() => Task.CompletedTask;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public T? Get<T>(CacheKey key, Func<T?> acquire) => acquire();

    public Task<T?> GetAsync<T>(CacheKey key, Func<Task<T?>> acquire) => acquire();

    public Task<T?> GetAsync<T>(CacheKey key, Func<T?> acquire) => Task.FromResult(acquire());

    public CacheKey PrepareKey(CacheKey cacheKey, params object[] cacheKeyParameters) => cacheKey;

    public CacheKey PrepareKeyForDefaultCache(CacheKey cacheKey, params object[] cacheKeyParameters) => cacheKey;

    public CacheKey PrepareKeyForShortTermCache(CacheKey cacheKey, params object[] cacheKeyParameters) => cacheKey;

    public Task RemoveAsync(CacheKey cacheKey, params object[] cacheKeyParameters) => Task.CompletedTask;

    public Task RemoveByPrefixAsync(string prefix, params object[] prefixParameters) => Task.CompletedTask;

    public Task SetAsync(CacheKey key, object data) => Task.CompletedTask;

    protected virtual void Dispose(bool disposing)
    {
        // nothing to do
    }
}