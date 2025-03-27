using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Musics;

public class GetMusicsQueryHandler : IRequestHandler<GetMusicsQuery, List<MusicModel>>
{
    private readonly IEntityRepository<MusicEntity> _musicRepository;
    private readonly IMapper _mapper;

    public GetMusicsQueryHandler(
        IEntityRepository<MusicEntity> musicRepository,
        IMapper mapper
    )
    {
        _musicRepository = musicRepository;
        _mapper = mapper;
    }

    public async Task<List<MusicModel>> Handle(GetMusicsQuery request, CancellationToken cancellationToken)
    {
        var musics = await _musicRepository
            .GetEntities()
            .Include(m => m.Genres)
            .ToListAsync();
        return _mapper.Map<List<MusicModel>>(musics);
    }
}