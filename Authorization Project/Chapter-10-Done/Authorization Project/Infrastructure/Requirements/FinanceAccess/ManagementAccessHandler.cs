using Microsoft.AspNetCore.Authorization;

internal sealed class ManagementAccessHandler : AuthorizationHandler<FinanceAccessRequirement>
{

    private readonly ILogger<ManagementAccessHandler> _logger;

    public ManagementAccessHandler(ILogger<ManagementAccessHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating management requirement for user '{context.User.Identity?.Name}'");

        var user = context.User;

        // If the user is in the managements role, grant access
        if (user.IsInRole("management"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

