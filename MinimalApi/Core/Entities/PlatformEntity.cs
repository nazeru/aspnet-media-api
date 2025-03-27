namespace MinimalApi.Core.Entities;

public class PlatformEntity : BaseEntity
{
    public string Name { get; set; }
    
    public ICollection<MediaEntity> Medias { get; set; }
}

public class PlatformModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}