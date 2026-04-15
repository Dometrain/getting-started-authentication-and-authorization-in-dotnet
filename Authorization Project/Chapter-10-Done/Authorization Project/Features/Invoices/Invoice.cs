namespace StartcodeAuthorization.Features.Invoices
{
    public class Invoice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? Status { get; set; }
    }
}
