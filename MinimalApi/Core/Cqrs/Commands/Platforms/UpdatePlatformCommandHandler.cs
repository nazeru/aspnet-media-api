using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class UpdatePlatformCommandHandler : IRequestHandler<UpdatePlatformCommand, PlatformModel>
{
    private readonly IEntityRepository<PlatformEntity> _platformRepository;
    private readonly IMapper _mapper;

    public UpdatePlatformCommandHandler(IEntityRepository<PlatformEntity> platformRepository, IMapper mapper)
    {
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    public async Task<PlatformModel> Handle(UpdatePlatformCommand request, CancellationToken cancellationToken)
    {
        var platform = await _platformRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (platform != null)
        {
            _mapper.Map(request, platform);
            await _platformRepository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<PlatformEntity, PlatformModel>(platform);
    }
}