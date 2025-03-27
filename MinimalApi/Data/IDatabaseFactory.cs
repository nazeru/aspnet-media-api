using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Data;

public interface IDatabaseFactory
{
    DbContext Get();
}