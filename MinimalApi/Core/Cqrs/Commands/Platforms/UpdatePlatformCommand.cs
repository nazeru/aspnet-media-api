using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class UpdatePlatformCommand : IRequest<PlatformModel>
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
}