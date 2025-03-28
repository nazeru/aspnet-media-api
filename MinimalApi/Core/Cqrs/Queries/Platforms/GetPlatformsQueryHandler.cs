using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Platforms;

public class GetPlatformsQueryHandler : IRequestHandler<GetPlatformsQuery, List<PlatformModel>>
{
    private readonly IEntityRepository<PlatformEntity> _platformRepository;
    private readonly IMapper _mapper;

    public GetPlatformsQueryHandler(IEntityRepository<PlatformEntity> platformRepository, IMapper mapper)
    {
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    public async Task<List<PlatformModel>> Handle(GetPlatformsQuery request, CancellationToken cancellationToken)
    {
        var platforms = await _platformRepository
            .GetAllAsync();
        return _mapper.Map<List<PlatformModel>>(platforms);
    }
}