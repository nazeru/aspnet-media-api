using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class PatchGenreCommandHandler : IRequestHandler<PatchGenreCommand, GenreModel>
{
    private readonly IEntityRepository<GenreEntity> _genreRepository;
    private readonly IMapper _mapper;

    public PatchGenreCommandHandler(IEntityRepository<GenreEntity> genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<GenreModel> Handle(PatchGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository
            .GetByIdAsync(request.Id);
        request.Content.ApplyTo(genre);
        await _genreRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GenreModel>(genre);
    }
}