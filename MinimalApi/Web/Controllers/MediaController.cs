using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Core.Cqrs.Commands.Medias.Movies;
using MinimalApi.Core.Cqrs.Commands.Medias.Musics;
using MinimalApi.Core.Cqrs.Queries.Medias;
using MinimalApi.Core.Cqrs.Queries.Medias.Movies;
using MinimalApi.Core.Cqrs.Queries.Medias.Musics;
using MinimalApi.Core.Entities;

namespace MinimalApi.Web.Controllers;

[Route("medias")]
public class MediaController
{
    private readonly IMediator _mediator;

    public MediaController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get Media collection
    /// </summary>
    [HttpGet]
    public async Task<List<MediaModel>> GetMediasAsync([FromQuery] GetMediasQuery getMediasQuery)
    {
        return await _mediator.Send(getMediasQuery);
    }
    
    [HttpGet("GetMediaById")]
    public async Task<MediaModel> GetMediaByIdAsync([FromBody] GetMediaByIdQuery getMediaByIdQuery)
    {
        return await _mediator.Send(getMediaByIdQuery);
    }
    
    [HttpGet("movies")]
    public async Task<List<MovieModel>> GetMoviesAsync([FromQuery] GetMoviesQuery getMoviesQuery)
    {
        return await _mediator.Send(getMoviesQuery);
    }

    [HttpGet("movies/GetMoviesById")]
    public async Task<MovieModel> GetMovieByIdAsync([FromBody] GetMovieByIdQuery getMovieByIdQuery)
    {
        return await _mediator.Send(getMovieByIdQuery);
    }
    
    [HttpGet("musics")]
    public async Task<List<MusicModel>> GetMusicAsync([FromQuery] GetMusicsQuery getMusicsQuery)
    {
        return await _mediator.Send(getMusicsQuery);
    }

    [HttpGet("musics/GetMusicById")]
    public async Task<MusicModel> GetMusicByIdAsync([FromBody] GetMusicByIdQuery getMusicByIdQuery)
    {
        return await _mediator.Send(getMusicByIdQuery);
    }
    
    [HttpPost("movies")]
    public async Task<MovieModel> CreateMovieAsync([FromBody] CreateMovieCommand createMovieCommand)
    {
        return await _mediator.Send(createMovieCommand);
    }
    
    [HttpPost("musics")]
    public async Task<MusicModel> CreateMusicAsync([FromBody] CreateMusicCommand createMusicCommand)
    {
        return await _mediator.Send(createMusicCommand);
    }

    [HttpDelete("movies")]
    public async Task DeleteMovieAsync([FromBody] DeleteMovieCommand deleteMovieCommand)
    {
        await _mediator.Send(deleteMovieCommand);
    }

    [HttpDelete("musics")]
    public async Task DeleteMusicAsync([FromBody] DeleteMusicCommand deleteMusicCommand)
    {
        await _mediator.Send(deleteMusicCommand);
    }

    [HttpPut("movies")]
    public async Task UpdateMovieAsync([FromBody] UpdateMovieCommand updateMovieCommand)
    {
        await _mediator.Send(updateMovieCommand);
    }

    [HttpPut("musics")]
    public async Task UpdateMusicAsync([FromBody] UpdateMusicCommand updateMusicCommand)
    {
        await _mediator.Send(updateMusicCommand);
    }

    [HttpPatch("movies")]
    public async Task PatchMovieAsync([FromBody] PatchMovieCommand patchMovieCommand)
    {
        await _mediator.Send(patchMovieCommand);
    }

    [HttpPatch("musics")]
    public async Task PatchMusicAsync([FromBody] PatchMusicCommand patchMusicCommand)
    {
        await _mediator.Send(patchMusicCommand);
    }
}