using System.Runtime.InteropServices;
using AutoMapper;
using FluentValidation;
using MediatR;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;
using MinimalApi.Core.Utils;

namespace MinimalApi.Core.Cqrs.Commands.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
{
    private readonly IEntityRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IEntityRepository<UserEntity> userRepository, IMapper mapper, IValidator<CreateUserCommand> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        request.Phone = DataNormalizer.NormalizePhone(request.Phone);

        // if (request.Password != request.ConfirmPassword)
        // {
        //     throw new ExternalException("Passwords do not match");
        // }
        var userEntity = _mapper.Map<UserEntity>(request);
        
        await _userRepository.InsertAsync(userEntity);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserModel>(userEntity);
    }
}