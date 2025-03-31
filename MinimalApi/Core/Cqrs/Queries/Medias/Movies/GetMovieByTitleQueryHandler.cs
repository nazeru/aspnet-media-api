using AutoMapper;
using MediatR;
using MinimalApi.Abstractions.Movies;
using MinimalApi.Connector;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Movies;

public class GetMovieByTitleQueryHandler : IRequestHandler<GetMovieByTitleQuery, MovieModel>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;
    private readonly IMapper _mapper;
    private readonly IOMDbApiRestClient  _apiRestClient;

    public GetMovieByTitleQueryHandler(IOMDbApiRestClient apiRestClient, IMapper mapper, IEntityRepository<MovieEntity> movieRepository)
    {
        _apiRestClient = apiRestClient;
        _mapper = mapper;
        _movieRepository = movieRepository;
    }

    public async Task<MovieModel> Handle(GetMovieByTitleQuery request, CancellationToken cancellationToken)
    {
        MovieModel movie;
        
        var movieEntity = _movieRepository
            .Query
            .Single(e => e.Name == request.Title);

        if (movieEntity == null)
        {
            var movieDto = await _apiRestClient.GetMovieByTitle("6bf5f74d",_mapper.Map<GetMovieByTitleModel>(request));
            movie =  _mapper.Map<MovieModel>(movieDto);
        }
        else
        {
            movie = _mapper.Map<MovieModel>(movieEntity);
        }
        
        Console.WriteLine(movie);
        
        return _mapper.Map<MovieModel>(movie);
    }
}