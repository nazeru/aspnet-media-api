using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Core.Cqrs.Commands;
using MinimalApi.Core.Cqrs.Commands.Users;
using MinimalApi.Core.Cqrs.Queries;
using MinimalApi.Core.Cqrs.Queries.Users;
using MinimalApi.Core.Entities;

namespace MinimalApi.Web.Controllers;

[Route("/users")]
public class UserController : ControllerBase 
{
    
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<UserModel>> GetUsersAsync([FromQuery] GetUsersQuery getUsersQuery)
    {
        return await _mediator.Send(getUsersQuery);
    }
    
    [HttpGet("GetUserById")]
    public async Task<UserModel> GetUserByIdAsync([FromQuery] GetUserByIdQuery getUserByIdQuery)
    {
        return await _mediator.Send(getUserByIdQuery);
    }

    [HttpPost]
    public async Task<UserModel> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
    {
        return await _mediator.Send(createUserCommand);
    }

    [HttpPut]
    public async Task<UserModel> UpdateUserAsync([FromBody] UpdateUserCommand updateUserCommand)
    {
        return await _mediator.Send(updateUserCommand);
    }

    [HttpPatch]
    public async Task<UserModel> PatchUserAsync([FromBody] PatchUserCommand updateUserCommand)
    {
        return await _mediator.Send(updateUserCommand);
    }

    [HttpDelete]
    public async Task DeleteUserAsync([FromQuery] DeleteUserCommand deleteUserCommand)
    {
        await _mediator.Send(deleteUserCommand);
    }
    
}