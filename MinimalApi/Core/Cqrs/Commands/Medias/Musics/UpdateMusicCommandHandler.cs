using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class UpdateMusicCommandHandler : IRequestHandler<UpdateMusicCommand, MusicModel>
{
    private readonly IEntityRepository<MusicEntity> _musicRepository;
    private readonly IMapper _mapper;

    public UpdateMusicCommandHandler(IEntityRepository<MusicEntity> musicRepository, IMapper mapper)
    {
        _musicRepository = musicRepository;
        _mapper = mapper;
    }

    public async Task<MusicModel> Handle(UpdateMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository
            .GetByIdAsync(request.Id);

        if (music != null)
        {
            _mapper.Map(request, music);
            await _musicRepository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<MusicEntity, MusicModel>(music);
    }
}