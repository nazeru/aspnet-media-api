using System.Runtime.InteropServices;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class CreateMusicCommandHandler : IRequestHandler<CreateMusicCommand, MusicModel>
{
    private readonly IEntityRepository<MusicEntity> _musicRepository;
    private readonly IEntityRepository<GenreEntity> _genreRepository;
    private readonly IEntityRepository<PlatformEntity> _platformRepository;
    private readonly IMapper _mapper;

    public CreateMusicCommandHandler(
        IEntityRepository<MusicEntity> musicRepository,
        IEntityRepository<GenreEntity> genreRepository,
        IEntityRepository<PlatformEntity> platformRepository,
        IMapper mapper)
    {
        _musicRepository = musicRepository;
        _genreRepository = genreRepository;
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    public async Task<MusicModel> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
    {
        // Получаем платформу по PlatformId
        var platform = await _platformRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.PlatformId, cancellationToken);
        if (platform == null)
        {
            throw new ExternalException("Platform not found");
        }
        
        // Получаем все жанры по их ID
        var genres = await _genreRepository
            .GetEntities()
            .Where(e => request.GenresId.Contains(e.Id))
            .ToListAsync(cancellationToken);
        if (genres == null || genres.Count == 0)
        {
            throw new ExternalException("Genres not found");
        }

        // Маппим запрос на MusicEntity
        var music = _mapper.Map<MusicEntity>(request);
        music.Platform = platform;
        music.Genres = genres;

        // Добавляем музыку в репозиторий
        _musicRepository.Add(music);
        await _musicRepository.SaveChangesAsync(cancellationToken);

        // Маппим MusicEntity обратно в MusicModel для ответа
        return _mapper.Map<MusicModel>(music);
    }
}
