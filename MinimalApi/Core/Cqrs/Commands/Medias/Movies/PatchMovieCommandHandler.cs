using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class PatchMovieCommandHandler : IRequestHandler<PatchMovieCommand, MovieModel>
{
    private readonly IEntityRepository<MovieEntity> _movieRepository;
    private readonly IMapper _mapper;

    public PatchMovieCommandHandler(IEntityRepository<MovieEntity> movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieModel> Handle(PatchMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        request.Content.ApplyTo(movie);
        await _movieRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<MovieModel>(movie);
    }
}