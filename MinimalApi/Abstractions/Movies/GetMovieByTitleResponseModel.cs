using System.Text.Json.Serialization;

namespace MinimalApi.Abstractions.Movies;

public class GetMovieByTitleResponseModel
{
    [JsonPropertyName("Title")]
    public string Name { get; set; } //Name
    [JsonPropertyName("Poster")]
    public string Image { get; set; } //Image
    public string RunTime { get; set; } //RunTime
    [JsonPropertyName("Genre")]
    public string Genres { get; set; } //Genres
}