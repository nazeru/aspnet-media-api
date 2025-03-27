using System.Data.Common;

namespace MinimalApi.Core.Entities;

public class GenreEntity : BaseEntity
{
    public string Name { get; set; }
    
    public ICollection<MediaEntity>? Medias { get; set; }
}

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}