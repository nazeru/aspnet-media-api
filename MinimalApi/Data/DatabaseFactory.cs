using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Data;

public class DatabaseFactory : IDatabaseFactory, IDisposable
{
    private readonly ApplicationContext _dataContext;

    public DatabaseFactory(ApplicationContext dataContext)
    {
        _dataContext = dataContext;
    }

    public DbContext Get() => _dataContext;

    public void Dispose() => _dataContext?.Dispose();
}