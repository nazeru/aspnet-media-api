using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class DeleteUserCommand : IRequest
{
    [Required]
    public int Id { get; set; }
}