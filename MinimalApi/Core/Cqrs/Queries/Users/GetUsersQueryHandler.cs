using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;

namespace MinimalApi.Core.Cqrs.Queries.Users;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserModel>>
{
    private readonly IEntityRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IEntityRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users =  await _userRepository
            .GetEntities()
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<UserModel>>(users);
    }
}