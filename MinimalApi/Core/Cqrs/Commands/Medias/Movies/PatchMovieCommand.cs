using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class PatchMovieCommand : IRequest<MovieModel>
{
    [Required]
    public int Id { get; set; }

    public JsonPatchDocument<MovieEntity> Content { get; set; }

    public PatchMovieCommand()
    {
    }

    public PatchMovieCommand(int id, JsonPatchDocument<MovieEntity> content)
    {
        Id = id;
        Content = content;
    }
}