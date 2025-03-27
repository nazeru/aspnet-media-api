using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias;

public class GetMediasQueryHandler : IRequestHandler<GetMediasQuery, List<MediaModel>>
{
    private readonly IEntityRepository<MediaEntity> _mediaRepository;
    private readonly IMapper _mapper;

    public GetMediasQueryHandler(
        IEntityRepository<MediaEntity> mediaRepository,
        IMapper mapper
    )
    {
        _mediaRepository = mediaRepository;
        _mapper = mapper;
    }

    public async Task<List<MediaModel>> Handle(GetMediasQuery request, CancellationToken cancellationToken)
    {
        var medias = await _mediaRepository
            .GetEntities()
            .Include(m => m.Genres)
            .ToListAsync();
        return _mapper.Map<List<MediaModel>>(medias);
    }
}