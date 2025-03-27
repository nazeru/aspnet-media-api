using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias;

public class GetMediaByIdQuery : IRequest<MediaModel>
{
    [Required]
    public int Id { get; set; }
}