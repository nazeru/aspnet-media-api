using Microsoft.EntityFrameworkCore;
using MinimalApi.Core;
using MinimalApi.Core.Entities;

namespace MinimalApi.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
        SavingChanges += DataContext_SavingChanges;
    }
    
    public DbSet<UserEntity> Users { get; set; }
    //public DbSet<MediaEntity> Medias { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<PlatformEntity> Platforms { get; set; }
    
    public DbSet<MusicEntity> Musics { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Использование TPT для каждого типа медиа (отдельные таблицы для каждого типа)
        modelBuilder.Entity<MovieEntity>()
            .ToTable("Movies");

        modelBuilder.Entity<MusicEntity>()
            .ToTable("Musics");

        // Связь многие-ко-многим: Media ↔ Genre
        modelBuilder.Entity<MediaEntity>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Medias);

        // Связь один-ко-многим: User ↔ Media
        modelBuilder.Entity<MediaEntity>()
            .HasOne(m => m.User)
            .WithMany(u => u.Medias)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь один-ко-многим: Platform ↔ Media
        modelBuilder.Entity<MediaEntity>()
            .HasOne(m => m.Platform)
            .WithMany(p => p.Medias)
            .HasForeignKey(m => m.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void DataContext_SavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries()
            .Where(entry => entry.State == EntityState.Modified || entry.State == EntityState.Added);

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entity)
            {
                entity.ModifiedOn = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = entity.ModifiedOn;
                }
            }
        }
    }
}