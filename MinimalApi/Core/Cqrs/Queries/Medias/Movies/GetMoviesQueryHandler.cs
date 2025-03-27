using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Movies;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieModel>>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(
        IEntityRepository<MovieEntity> movieRepository,
        IMapper mapper
    )
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<List<MovieModel>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository
            .GetEntities()
            .Include(m => m.Genres)
            .ToListAsync();
        return _mapper.Map<List<MovieModel>>(movies);
    }
}