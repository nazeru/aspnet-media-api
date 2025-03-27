using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Core.Cqrs.Commands.Platforms;
using MinimalApi.Core.Cqrs.Queries.Platforms;
using MinimalApi.Core.Entities;

namespace MinimalApi.Web.Controllers;

[Route("Platforms")]
public class PlatformController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlatformController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<PlatformModel>> GetPlatformsAsync([FromQuery] GetPlatformsQuery getPlatformsQuery)
    {
        return await _mediator.Send(getPlatformsQuery);
    }
    
    [HttpGet("GetPlatformById")]
    public async Task<PlatformModel> GetPlatformByIdAsync([FromQuery] GetPlatformByIdQuery getPlatformByIdQuery)
    {
        return await _mediator.Send(getPlatformByIdQuery);
    }

    [HttpPost]
    public async Task<PlatformModel> CreatePlatformAsync([FromBody] CreatePlatformCommand createPlatformCommand)
    {
        return await _mediator.Send(createPlatformCommand);
    }
    
    [HttpPut]
    public async Task<PlatformModel> UpdatePlatformAsync([FromBody] UpdatePlatformCommand updatePlatformCommand)
    {
        return await _mediator.Send(updatePlatformCommand);
    }

    [HttpPatch]
    public async Task<PlatformModel> PatchPlatformAsync([FromBody] PatchPlatformCommand updatePlatformCommand)
    {
        return await _mediator.Send(updatePlatformCommand);
    }

    [HttpDelete]
    public async Task DeletePlatformAsync([FromQuery] DeletePlatformCommand deletePlatformCommand)
    {
        await _mediator.Send(deletePlatformCommand);
    }
}