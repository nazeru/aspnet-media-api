using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias;

public class GetMediasQuery : IRequest<List<MediaModel>>
{
    
}