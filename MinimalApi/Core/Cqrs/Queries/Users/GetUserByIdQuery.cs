using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Users;

public class GetUserByIdQuery : IRequest<UserModel>
{
    [Required]
    public int Id { get; set; }
}