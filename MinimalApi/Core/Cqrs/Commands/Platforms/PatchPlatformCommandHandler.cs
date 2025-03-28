using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class PatchPlatformCommandHandler : IRequestHandler<PatchPlatformCommand, PlatformModel>
{
    private readonly IEntityRepository<PlatformEntity> _platformRepository;
    private readonly IMapper _mapper;

    public PatchPlatformCommandHandler(IEntityRepository<PlatformEntity> platformRepository, IMapper mapper)
    {
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    public async Task<PlatformModel> Handle(PatchPlatformCommand request, CancellationToken cancellationToken)
    {
        var platform = await _platformRepository
            .GetByIdAsync(request.Id);
        request.Content.ApplyTo(platform);
        await _platformRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PlatformModel>(platform);
    }
}