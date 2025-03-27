using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class PatchUserCommandHandler : IRequestHandler<PatchUserCommand, UserModel>
{
    private readonly IEntityRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public PatchUserCommandHandler(IEntityRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        request.Content.ApplyTo(user);
        await _userRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserModel>(user);
    }
}