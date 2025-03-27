using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Musics;

public class GetMusicsQuery : IRequest<List<MusicModel>>
{
    
}