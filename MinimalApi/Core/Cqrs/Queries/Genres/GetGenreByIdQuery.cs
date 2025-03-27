using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Genres;

public class GetGenreByIdQuery : IRequest<GenreModel>
{
    public int Id { get; set; }
}