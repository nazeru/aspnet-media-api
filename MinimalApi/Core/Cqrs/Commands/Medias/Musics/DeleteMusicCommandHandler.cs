using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class DeleteMusicCommandHandler : IRequestHandler<DeleteMusicCommand>
{
    private readonly IEntityRepository<MusicEntity> _musicRepository;

    public DeleteMusicCommandHandler(IEntityRepository<MusicEntity> musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public async Task Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
    {
        var music = await _musicRepository
            .GetByIdAsync(request.Id);
        await _musicRepository.DeleteAsync(music);
        await _musicRepository.SaveChangesAsync(cancellationToken);
    }
}