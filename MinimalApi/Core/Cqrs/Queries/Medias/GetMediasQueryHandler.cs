using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias;

public class GetMediasQueryHandler : IRequestHandler<GetMediasQuery, List<MediaModel>>
{
    private readonly IEntityRepository<MediaEntity> _mediaRepository;
    private readonly IEntityRepository<GenreEntity> _genreRepository;
    private readonly IMapper _mapper;

    public GetMediasQueryHandler(
        IEntityRepository<MediaEntity> mediaRepository,
        IEntityRepository<GenreEntity> genreRepository,
        IMapper mapper
    )
    {
        _mediaRepository = mediaRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<List<MediaModel>> Handle(GetMediasQuery request, CancellationToken cancellationToken)
    {
        var query = _mediaRepository.Query
            .Include(e => e.Genres);

        return _mapper.Map<List<MediaModel>>(await query.ToListAsync(cancellationToken));
    }
}