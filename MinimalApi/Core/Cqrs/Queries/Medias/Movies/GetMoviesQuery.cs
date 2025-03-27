using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Movies;

public class GetMoviesQuery : IRequest<List<MovieModel>>
{
    
}