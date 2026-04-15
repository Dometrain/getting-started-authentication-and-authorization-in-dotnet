using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Invoices
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceDatabase invoiceDatabase;

        public InvoicesController(IInvoiceDatabase invoiceDatabase)
        {
            this.invoiceDatabase = invoiceDatabase;
        }

        [Authorize]
        public ActionResult<Invoice> Invoice(int id)
        {
            // 1. Load the invoice (resource)
            var invoice = invoiceDatabase.GetInvoiceById(id);

            if (invoice == null)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }
            else
            {
                return invoice;
            }
        }
    }
}
