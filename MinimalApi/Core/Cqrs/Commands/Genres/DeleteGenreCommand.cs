using MediatR;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class DeleteGenreCommand : IRequest
{
    public int Id { get; set; }
}