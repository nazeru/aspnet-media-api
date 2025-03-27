using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class CreateUserCommand : IRequest<UserModel>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
    //public List<MediaEntity>? Medias { get; set; }
}