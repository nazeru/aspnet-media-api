using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Core.Cqrs.Commands.Genres;
using MinimalApi.Core.Cqrs.Queries.Genres;
using MinimalApi.Core.Entities;

namespace MinimalApi.Web.Controllers;

[Route("Genres")]
public class GenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<GenreModel>> GetGenresAsync([FromQuery] GetGenresQuery getGenresQuery)
    {
        return await _mediator.Send(getGenresQuery);
    }
    
    [HttpGet("GetGenreById")]
    public async Task<GenreModel> GetGenreByIdAsync([FromQuery] GetGenreByIdQuery getGenreByIdQuery)
    {
        return await _mediator.Send(getGenreByIdQuery);
    }

    [HttpPost]
    public async Task<GenreModel> CreateGenreAsync([FromBody] CreateGenreCommand createGenreCommand)
    {
        return await _mediator.Send(createGenreCommand);
    }
    
    [HttpPut]
    public async Task<GenreModel> UpdateGenreAsync([FromBody] UpdateGenreCommand updateGenreCommand)
    {
        return await _mediator.Send(updateGenreCommand);
    }

    [HttpPatch]
    public async Task<GenreModel> PatchGenreAsync([FromBody] PatchGenreCommand updateGenreCommand)
    {
        return await _mediator.Send(updateGenreCommand);
    }

    [HttpDelete]
    public async Task DeleteGenreAsync([FromQuery] DeleteGenreCommand deleteGenreCommand)
    {
        await _mediator.Send(deleteGenreCommand);
    }
}