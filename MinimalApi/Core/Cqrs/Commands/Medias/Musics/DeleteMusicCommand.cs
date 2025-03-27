using MediatR;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Musics;

public class DeleteMusicCommand : IRequest
{
    public int Id { get; set; }
}