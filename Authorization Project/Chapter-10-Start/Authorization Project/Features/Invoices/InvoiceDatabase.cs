namespace StartcodeAuthorization.Features.Invoices
{
    public class InvoiceDatabase : IInvoiceDatabase
    {
        private static readonly List<Invoice> _invoices =
            [
                new Invoice { Id = 1, UserId = 1, InvoiceNumber = "INV-001", Description = "Office Supplies", Amount = 250.00m, InvoiceDate = DateTime.Now.AddDays(-10), Status = "Paid" },
                new Invoice { Id = 2, UserId = 1, InvoiceNumber = "INV-002", Description = "Software License", Amount = 1200.00m, InvoiceDate = DateTime.Now.AddDays(-5), Status = "Pending" },
                new Invoice { Id = 3, UserId = 2, InvoiceNumber = "INV-003", Description = "Marketing Materials", Amount = 450.00m, InvoiceDate = DateTime.Now.AddDays(-8), Status = "Paid" },
                new Invoice { Id = 4, UserId = 2, InvoiceNumber = "INV-004", Description = "Travel Expenses", Amount = 800.00m, InvoiceDate = DateTime.Now.AddDays(-3), Status = "Pending" },
                new Invoice { Id = 5, UserId = 3, InvoiceNumber = "INV-005", Description = "Equipment Rental", Amount = 600.00m, InvoiceDate = DateTime.Now.AddDays(-1), Status = "Draft" },
                new Invoice { Id = 6, UserId = 3, InvoiceNumber = "INV-006", Description = "Equipment Purchase", Amount = 2600.00m, InvoiceDate = DateTime.Now.AddDays(-21), Status = "Draft" }
            ];

        public Invoice? GetInvoiceById(int id)
        {
            return _invoices.FirstOrDefault(i => i.Id == id);
        }

        public List<Invoice> GetInvoicesByUserId(int userId)
        {
            return _invoices.Where(i => i.UserId == userId).ToList();
        }

        public List<Invoice> GetAllInvoices()
        {
            return _invoices.ToList();
        }
    }
}
