using AutoMapper;
using MediatR;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, GenreModel>
{
    private readonly IMapper _mapper;
    private readonly IEntityRepository<GenreEntity> _genreRepository;

    public CreateGenreCommandHandler(IMapper mapper, IEntityRepository<GenreEntity> genreRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
    }

    public async Task<GenreModel> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = _mapper.Map<GenreEntity>(request);
        await _genreRepository.InsertAsync(genre);
        await _genreRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GenreModel>(genre);
    }
}