using Refit;

namespace MinimalApi.Abstractions.Movies;

public enum PlotEnum 
{
    Short,
    Full
}

public class GetMovieByTitleModel
{
    [AliasAs("t")]
    public string Title { get; set; }
    [AliasAs("y")]
    public int? Year { get; set; }
    public PlotEnum Plot { get; set; } = PlotEnum.Short;
}