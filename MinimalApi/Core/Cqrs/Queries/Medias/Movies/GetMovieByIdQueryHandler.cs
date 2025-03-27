using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Movies;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieModel>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieByIdQueryHandler(IEntityRepository<MovieEntity> movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }
    
    public async Task<MovieModel> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        
        return _mapper.Map<MovieModel>(movie);
    }
}