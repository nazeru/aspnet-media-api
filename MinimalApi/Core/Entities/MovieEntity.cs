namespace MinimalApi.Core.Entities;

public class MovieEntity : MediaEntity
{
    public int RunTime { get; set; }
    public bool Franchise { get; set; }

    public override string MediaType => "movie";
}

public class MovieModel : MediaModel
{
    public string RunTime { get; set; }
    public bool Franchise { get; set; }
}