using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Data.Repositories;

namespace MinimalApi.Data;

public class BaseEntityRepository<T> : EntityRepository<T> where T : class
{
    public BaseEntityRepository(IDatabaseFactory databaseFactory)
        : base(databaseFactory)
    {
        ((ApplicationContext) Context).SavingChanges += OnSavingChanges;
    }

    private void OnSavingChanges(object sender, EventArgs eventArgs)
    {
        var entries = Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
            .Where(e => e.Entity is T)
            .Select(e => new { Entity = (T) e.Entity, State = e.State })
            .ToList();

        if (entries.Count > 0)
        {
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.ModifiedOn = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        baseEntity.CreatedOn = baseEntity.ModifiedOn;
                    }
                }
            }
        }
    }
}