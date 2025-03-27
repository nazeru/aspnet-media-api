using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Platforms;

public class GetPlatformsQuery : IRequest<List<PlatformModel>>
{
    
}