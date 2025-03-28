using System.Runtime.InteropServices;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieModel>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;
    private readonly IEntityRepository<GenreEntity> _genreRepository;
    private readonly IEntityRepository<PlatformEntity> _platformRepository;
    private readonly IMapper _mapper;

    public CreateMovieCommandHandler(
        IEntityRepository<MovieEntity> movieRepository,
        IEntityRepository<GenreEntity> genreRepository,
        IEntityRepository<PlatformEntity> platformRepository,
        IMapper mapper)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    public async Task<MovieModel> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        // Получаем платформу по PlatformId
        var platform = await _platformRepository
            .GetByIdAsync(request.PlatformId);
        if (platform == null)
        {
            throw new ExternalException("Platform not found");
        }
        
        // Получаем все жанры по их ID
        var genres = await _genreRepository
            .GetByIdsAsync(request.GenresId);
        if (genres == null || genres.Count == 0)
        {
            throw new ExternalException("Genres not found");
        }

        // Маппим запрос на MovieEntity
        var movie = _mapper.Map<MovieEntity>(request);
        movie.Platform = platform;
        movie.Genres = genres;

        // Добавляем фильм в репозиторий
        await _movieRepository.InsertAsync(movie);
        await _movieRepository.SaveChangesAsync(cancellationToken);

        // Маппим MovieEntity обратно в MovieModel для ответа
        return _mapper.Map<MovieModel>(movie);
    }
}
