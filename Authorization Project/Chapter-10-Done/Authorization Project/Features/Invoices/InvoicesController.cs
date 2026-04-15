using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Invoices
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceDatabase invoiceDatabase;
        private readonly IAuthorizationService authorizationService;

        public InvoicesController(IInvoiceDatabase invoiceDatabase, IAuthorizationService authorizationService)
        {
            this.invoiceDatabase = invoiceDatabase;
            this.authorizationService = authorizationService;
        }

        [Authorize]
        public async Task<ActionResult<Invoice>> Invoice(int id)
        {
            // 1. Load the invoice (resource)
            var invoice = invoiceDatabase.GetInvoiceById(id);

            if (invoice == null)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }
            else
            {
                // 2. Check authorization with the resource
                var authResult = await authorizationService.AuthorizeAsync(
                    user: User,
                    resource: invoice,
                    policyName: "InvoiceOwner"
                );

                if (!authResult.Succeeded)
                {
                    return Forbid();
                }

                // 3. Authorization passed, return the invoice
                return invoice;
            }
        }
    }
}
