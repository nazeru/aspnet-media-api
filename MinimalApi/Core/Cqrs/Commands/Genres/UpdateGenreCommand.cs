using System.ComponentModel.DataAnnotations;
using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Commands.Genres;

public class UpdateGenreCommand : IRequest<GenreModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
}