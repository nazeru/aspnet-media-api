using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Medias.Movies;

public class GetMovieByIdQuery : IRequest<MovieModel>
{
    [Required]
    public int Id { get; set; }
}