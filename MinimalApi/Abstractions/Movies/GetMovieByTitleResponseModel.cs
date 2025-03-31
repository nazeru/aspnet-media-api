namespace MinimalApi.Abstractions.Movies;

public class GetMovieByTitleResponseModel
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Rated { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string RunTime { get; set; }
    public string Genre { get; set; }
    public string Poster { get; set; }
}