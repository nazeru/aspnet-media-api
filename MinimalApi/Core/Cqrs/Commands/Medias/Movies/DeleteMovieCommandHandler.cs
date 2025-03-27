using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;

    public DeleteMovieCommandHandler(IEntityRepository<MovieEntity> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        _movieRepository.Delete(movie);
        await _movieRepository.SaveChangesAsync(cancellationToken);
    }
}