using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class CreateMusicCommand : IRequest<MusicModel>
{
    public string Name { get ; set ; }
    public string Image { get ; set ; }
    public int PlatformId { get ; set ; }
    public ICollection<int> GenresId { get; set; }
    public string Band  { get ; set ; }
}