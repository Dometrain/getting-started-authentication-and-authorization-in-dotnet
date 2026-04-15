using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

internal sealed class ActiveUserHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    private readonly ILogger<ActiveUserHandler> _logger;

    public ActiveUserHandler(ILogger<ActiveUserHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating if active user, for user '{context.User.Identity?.Name}'");


        var user = context.User;

        var isActive = IsUserActiveInDatabase(user);

        if (isActive == false)
        {
            context.Fail(); // Block access for inactive users
        }

        return Task.CompletedTask;
    }

    private bool IsUserActiveInDatabase(ClaimsPrincipal user)
    {
        return true;
    }
}

