using Microsoft.EntityFrameworkCore;
using MinimalApi.Shared;

namespace MinimalApi.Data.Extensions;

public static class IQueriableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, bool getOnlyTotalCount = false)
    {
        if (source == null)
        {
            return new PagedList<T>(new List<T>(), pageIndex, pageSize);
        }

        pageSize = Math.Max(pageSize, 1);

        var count = await source.CountAsync();

        var items = new List<T>();

        if (!getOnlyTotalCount)
        {
            items.AddRange(await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync());
        }

        return new PagedList<T>(items, pageIndex, pageSize, count);
    }
}