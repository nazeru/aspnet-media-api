using MediatR;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class DeleteMovieCommand : IRequest
{
    public int Id { get; set; }
}