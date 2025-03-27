using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class PatchPlatformCommand : IRequest<PlatformModel>
{
    [Required]
    public int Id { get; set; }

    public JsonPatchDocument<PlatformEntity> Content { get; set; }

    public PatchPlatformCommand()
    {
    }

    public PatchPlatformCommand(int id, JsonPatchDocument<PlatformEntity> content)
    {
        Id = id;
        Content = content;
    }
}