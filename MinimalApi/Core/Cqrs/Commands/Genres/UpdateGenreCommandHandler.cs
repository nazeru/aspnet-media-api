using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreModel>
{
    private readonly IEntityRepository<GenreEntity> _genreRepository;
    private readonly IMapper _mapper;

    public UpdateGenreCommandHandler(IEntityRepository<GenreEntity> genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<GenreModel> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository
            .GetByIdAsync(request.Id);

        if (genre != null)
        {
            _mapper.Map(request, genre);
            await _genreRepository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<GenreEntity, GenreModel>(genre);
    }
}