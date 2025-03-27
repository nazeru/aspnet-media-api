using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Musics;

public class GetMusicByIdQuery : IRequest<MusicModel>
{
    [Required]
    public int Id { get; set; }
}