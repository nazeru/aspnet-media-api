using Microsoft.AspNetCore.Components.RenderTree;
using MinimalApi.Abstractions.Movies;
using Refit;

namespace MinimalApi.Connector;

[Headers("Content-Type: application/json")]
public interface IOMDbApiRestClient
{
    [Get("/")]
    Task<GetMovieByTitleResponseModel> GetMovieByTitle(GetMovieByTitleModel getMovieByTitleModel);
}