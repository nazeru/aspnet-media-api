using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class CreateGenreCommand : IRequest<GenreModel>
{
    public string Name { get; set; }
}