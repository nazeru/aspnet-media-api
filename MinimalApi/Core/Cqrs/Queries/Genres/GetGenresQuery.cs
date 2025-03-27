using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Genres;

public class GetGenresQuery : IRequest<List<GenreModel>>
{
    
}