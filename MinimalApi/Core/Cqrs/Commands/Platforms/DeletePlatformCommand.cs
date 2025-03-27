using MediatR;

namespace MinimalApi.Core.Cqrs.Commands.Platforms;

public class DeletePlatformCommand : IRequest
{
    public int Id { get; set; }
}