
namespace StartcodeAuthorization.Features.Invoices
{
    public interface IInvoiceDatabase
    {
        List<Invoice> GetAllInvoices();
        Invoice? GetInvoiceById(int id);
        List<Invoice> GetInvoicesByUserId(int userId);
    }
}