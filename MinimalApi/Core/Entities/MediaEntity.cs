using Microsoft.AspNetCore.Mvc.Formatters;

namespace MinimalApi.Core.Entities;

public interface IMediaEntity
{
    public string MediaType { get; }
}

public abstract class MediaEntity : BaseEntity
{
    public string Name { get; set; }
    
    public string Image { get; set; }
    
    public int? UserId { get; set; }
    public UserEntity? User { get; set; }

    public int PlatformId { get; set; }
    public PlatformEntity Platform { get; set; }

    public ICollection<GenreEntity> Genres { get; set; }
    
    public abstract string MediaType { get; }
    
}

public class MediaModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int UserId { get; set; }
    public int PlatformId { get; set; }
    public ICollection<GenreModel> Genres { get; set; }
    public string MediaType { get; set; }
}