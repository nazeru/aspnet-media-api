using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
{
    private readonly IEntityRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IEntityRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetEntities()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (user != null)
        {
            _mapper.Map(request, user);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<UserEntity, UserModel>(user);
    }
}