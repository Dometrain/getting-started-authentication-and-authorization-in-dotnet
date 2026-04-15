using Microsoft.AspNetCore.Authorization;
using StartcodeAuthorization.Features.Invoices;

internal class InvoiceOwnershipHandler : AuthorizationHandler<InvoiceOwnershipRequirement, Invoice>
{

    public InvoiceOwnershipHandler()
    {
        Console.WriteLine("InvoiceOwnershipHandler.Constructor");
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   InvoiceOwnershipRequirement requirement,
                                                   Invoice invoice)
    {
        // Get current user ID from claims
        var currentUserIdClaim = context.User.FindFirst("sub")?.Value;

        if (currentUserIdClaim == null)
        {
            // User not authenticated
            return Task.CompletedTask;
        }

        if (int.TryParse(currentUserIdClaim, out int currentUserId))
        {
            // Check if user owns this invoice
            if (invoice.UserId == currentUserId)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}