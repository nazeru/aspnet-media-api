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
            .GetByIdAsync(request.Id);
        await _genreRepository.DeleteAsync(genre);
        await _genreRepository.SaveChangesAsync(cancellationToken);
    }
}