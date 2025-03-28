using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class PatchMusicCommandHandler : IRequestHandler<PatchMusicCommand, MusicModel>
{
    private readonly IEntityRepository<MusicEntity> _musicRepository;
    private readonly IMapper _mapper;

    public PatchMusicCommandHandler(IEntityRepository<MusicEntity> musicRepository, IMapper mapper)
    {
        _musicRepository = musicRepository;
        _mapper = mapper;
    }

    public async Task<MusicModel> Handle(PatchMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository
            .GetByIdAsync(request.Id);
        request.Content.ApplyTo(music);
        await _musicRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<MusicModel>(music);
    }
}