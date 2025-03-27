using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Genres;

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<GenreModel>>
{
    private readonly IEntityRepository<GenreEntity> _genreRepository;
    private readonly IMapper _mapper;

    public GetGenresQueryHandler(IEntityRepository<GenreEntity> genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<List<GenreModel>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository
            .GetEntities()
            .ToListAsync();
        return _mapper.Map<List<GenreModel>>(genres);
    }
}