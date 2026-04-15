using Microsoft.AspNetCore.Authorization;

internal sealed class FinanceAccessHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    private readonly ILogger<FinanceAccessHandler> _logger;

    public FinanceAccessHandler(ILogger<FinanceAccessHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating finance requirement for user '{context.User.Identity?.Name}'");

        var user = context.User;

        var titleOk = user.HasClaim("JobTitle", "finance");

        var countryOk = user.HasClaim("country", "Sweden") ||
                        user.HasClaim("country", "Denmark");

        var hasFinanceRole = user.IsInRole("finance");

        if (hasFinanceRole && titleOk && countryOk)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

