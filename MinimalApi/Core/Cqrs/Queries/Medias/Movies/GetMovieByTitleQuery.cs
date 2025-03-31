using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Movies;

public class GetMovieByTitleQuery : IRequest<MovieModel>
{
    public string Title { get; set; }
}