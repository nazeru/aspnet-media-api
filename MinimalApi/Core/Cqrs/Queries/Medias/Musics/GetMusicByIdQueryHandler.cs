using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Musics;

public class GetMusicByIdQueryHandler : IRequestHandler<GetMusicByIdQuery, MusicModel>
{
    private readonly IEntityRepository<MusicEntity> _musicRepository;
    private readonly IMapper _mapper;

    public GetMusicByIdQueryHandler(IEntityRepository<MusicEntity> musicRepository, IMapper mapper)
    {
        _musicRepository = musicRepository;
        _mapper = mapper;
    }
    
    public async Task<MusicModel> Handle(GetMusicByIdQuery request, CancellationToken cancellationToken)
    {
        var music = _musicRepository
            .Query
            .Include(e => e.Genres)
            .Single(e => e.Id == request.Id);
        
        return _mapper.Map<MusicModel>(music);
    }
}