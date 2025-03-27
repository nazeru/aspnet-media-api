using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IEntityRepository<GenreEntity> _genreRepository;

    public DeleteGenreCommandHandler(IEntityRepository<GenreEntity> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        _genreRepository.Delete(genre);
        await _genreRepository.SaveChangesAsync(cancellationToken);
    }
}