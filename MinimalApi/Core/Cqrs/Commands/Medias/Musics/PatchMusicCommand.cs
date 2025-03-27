using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class PatchMusicCommand : IRequest<MusicModel>
{
    [Required]
    public int Id { get; set; }

    public JsonPatchDocument<MusicEntity> Content { get; set; }

    public PatchMusicCommand()
    {
    }

    public PatchMusicCommand(int id, JsonPatchDocument<MusicEntity> content)
    {
        Id = id;
        Content = content;
    }
}