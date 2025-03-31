namespace MinimalApi.Abstractions.Movies;

public enum PlotEnum 
{
    Short,
    Full
}

public class GetMovieByTitleModel
{
    public string Title { get; set; }
    public int Year { get; set; }
    public PlotEnum Plot { get; set; } = PlotEnum.Short;
}