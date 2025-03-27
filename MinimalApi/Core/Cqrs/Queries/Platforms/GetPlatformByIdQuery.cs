using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Platforms;

public class GetPlatformByIdQuery : IRequest<PlatformModel>
{
    public int Id { get; set; }
}