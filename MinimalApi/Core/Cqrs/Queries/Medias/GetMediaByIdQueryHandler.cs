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
        var media = _mediaRepository
            .Query
            .Include(e => e.Genres)
            .Single(e => e.Id == request.Id);
        
        return _mapper.Map<MediaModel>(media);
    }
}