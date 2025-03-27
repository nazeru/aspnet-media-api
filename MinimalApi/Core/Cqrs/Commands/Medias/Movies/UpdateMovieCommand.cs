using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Medias.Movies;

public class UpdateMovieCommand : IRequest<MovieModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int RunTime { get; set; }
    public bool Franchise { get; set; }
}