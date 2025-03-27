using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class CreateMovieCommand : IRequest<MovieModel>
{
    public string Name { get ; set ; }
    public string Image { get ; set ; }
    public int PlatformId { get ; set ; }
    public ICollection<int> GenresId { get; set; }
    public int RunTime { get ; set ; }
    public bool Franchise { get ; set ; }
}