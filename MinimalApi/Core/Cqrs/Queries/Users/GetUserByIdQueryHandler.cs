using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
{
    private readonly IEntityRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IEntityRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdAsync(request.Id);
        
        return _mapper.Map<UserModel>(user);
    }
}