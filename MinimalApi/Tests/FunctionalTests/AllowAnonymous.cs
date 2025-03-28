using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Tests.FunctionalTests;

public class AllowAnonymous : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
        {
            context.Succeed(requirement); //Simply pass all requirements
        }

        return Task.CompletedTask;
    }
}