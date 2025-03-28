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
            .GetByIdAsync(request.Id);
        await _platformRepository.DeleteAsync(platform);
        await _platformRepository.SaveChangesAsync(cancellationToken);
    }
}