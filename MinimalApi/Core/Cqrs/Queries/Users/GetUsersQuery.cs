using MediatR;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Cqrs.Queries.Users;

public class GetUsersQuery : IRequest<List<UserModel>>
{
    // public int? PageIndex { get; set; }
    // public int? PageSize { get; set; }
    //public string OrderBy { get; set; }
}