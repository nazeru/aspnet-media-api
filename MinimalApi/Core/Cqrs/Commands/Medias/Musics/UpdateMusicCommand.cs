using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class UpdateMusicCommand : IRequest<MusicModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Band  { get; set; }
}