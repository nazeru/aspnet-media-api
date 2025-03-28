using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IEntityRepository<UserEntity> _userRepository;

    public DeleteUserCommandHandler(IEntityRepository<UserEntity> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdAsync(request.Id);
        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}