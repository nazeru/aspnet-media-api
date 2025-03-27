using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Platforms;

public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, PlatformModel>
{
    private readonly IMapper _mapper;
    private readonly IEntityRepository<PlatformEntity> _platformRepository;

    public GetPlatformByIdQueryHandler(IMapper mapper, IEntityRepository<PlatformEntity> platformRepository)
    {
        _mapper = mapper;
        _platformRepository = platformRepository;
    }

    public async Task<PlatformModel> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
    {
        var platform = await _platformRepository
            .GetEntities()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return _mapper.Map<PlatformModel>(platform);
    }
}