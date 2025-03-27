using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class PatchUserCommand : IRequest<UserModel>
{
    [Required]
    public int Id { get; set; }

    public JsonPatchDocument<UserEntity> Content { get; set; }

    public PatchUserCommand()
    {
    }

    public PatchUserCommand(int id, JsonPatchDocument<UserEntity> content)
    {
        Id = id;
        Content = content;
    }
}