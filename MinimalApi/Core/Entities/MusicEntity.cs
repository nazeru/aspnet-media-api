namespace MinimalApi.Core.Entities;

public class MusicEntity : MediaEntity
{
    public string Band  { get; set; }
    public override string MediaType => "music";
}

public class MusicModel : MediaModel
{
    public string Band  { get; set; }
}