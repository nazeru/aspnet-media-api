using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Genres;

public class GetGenresByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreModel>
{
    private readonly IMapper _mapper;
    private readonly IEntityRepository<GenreEntity> _genreRepository;

    public GetGenresByIdQueryHandler(IMapper mapper, IEntityRepository<GenreEntity> genreRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
    }

    public async Task<GenreModel> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository
            .GetByIdAsync(request.Id);
        return _mapper.Map<GenreModel>(genre);
    }
}