using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias;

public class GetMediaByIdQueryHandler : IRequestHandler<GetMediaByIdQuery, MediaModel>
{
    private readonly IEntityRepository<MediaEntity> _mediaRepository;
    private readonly IMapper _mapper;

    public GetMediaByIdQueryHandler(IEntityRepository<MediaEntity> mediaRepository, IMapper mapper)
    {
        _mediaRepository = mediaRepository;
        _mapper = mapper;
    }
    
    public async Task<MediaModel> Handle(GetMediaByIdQuery request, CancellationToken cancellationToken)
    {
        var media = await _mediaRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        
        return _mapper.Map<MediaModel>(media);
    }
}