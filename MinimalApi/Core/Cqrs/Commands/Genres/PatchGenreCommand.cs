using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class PatchGenreCommand : IRequest<GenreModel>
{
    [Required]
    public int Id { get; set; }

    public JsonPatchDocument<GenreEntity> Content { get; set; }

    public PatchGenreCommand()
    {
    }

    public PatchGenreCommand(int id, JsonPatchDocument<GenreEntity> content)
    {
        Id = id;
        Content = content;
    }
}