using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, MovieModel>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;
    private readonly IMapper _mapper;

    public UpdateMovieCommandHandler(IEntityRepository<MovieEntity> movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieModel> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository
            .GetByIdAsync(request.Id);

        if (movie != null)
        {
            _mapper.Map(request, movie);
            await _movieRepository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<MovieEntity, MovieModel>(movie);
    }
}