using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class DeletePlatformCommandHandler : IRequestHandler<DeletePlatformCommand>
{
    private readonly IEntityRepository<PlatformEntity> _platformRepository;

    public DeletePlatformCommandHandler(IEntityRepository<PlatformEntity> platformRepository)
    {
        _platformRepository = platformRepository;
    }

    public async Task Handle(DeletePlatformCommand request, CancellationToken cancellationToken)
    {
        var platform = await _platformRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        _platformRepository.Delete(platform);
        await _platformRepository.SaveChangesAsync(cancellationToken);
    }
}