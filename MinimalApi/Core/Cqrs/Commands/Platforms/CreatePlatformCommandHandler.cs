using AutoMapper;
using MediatR;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, PlatformModel>
{
    private readonly IMapper _mapper;
    private readonly IEntityRepository<PlatformEntity> _platformRepository;

    public CreatePlatformCommandHandler(IMapper mapper, IEntityRepository<PlatformEntity> platformRepository)
    {
        _mapper = mapper;
        _platformRepository = platformRepository;
    }

    public async Task<PlatformModel> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
    {
        var platform = _mapper.Map<PlatformEntity>(request);
        await _platformRepository.InsertAsync(platform);
        await _platformRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PlatformModel>(platform);
    }
}