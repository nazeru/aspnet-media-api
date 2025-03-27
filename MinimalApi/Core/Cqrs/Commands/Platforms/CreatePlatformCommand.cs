using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class CreatePlatformCommand : IRequest<PlatformModel>
{
    public string Name { get; set; }
}