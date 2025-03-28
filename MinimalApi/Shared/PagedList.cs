namespace MinimalApi.Shared;

public class PagedList<T>
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="items">Список элементов</param>
    /// <param name="pageIndex">Индекс страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="totalItems">Общее кол-во записей</param>
    public PagedList(IList<T> items, int pageIndex, int pageSize, int? totalItems = null)
    {
        if (pageIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex));
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        TotalItems = totalItems ?? items.Count;
        TotalPages = TotalItems / pageSize;

        if (TotalItems % pageSize > 0)
        {
            TotalPages++;
        }

        PageSize = pageSize;
        PageIndex = pageIndex;

        Items = totalItems != null ? items :
            items.Skip(pageIndex * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// Индекс страницы
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Кол-во страниц
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Общее кол-во записей
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    /// Записи на странице
    /// </summary>
    public IList<T> Items { get; }
}