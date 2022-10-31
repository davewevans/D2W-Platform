namespace D2W.WebPortal.DTOs
{
    public class InvoiceItemDto
    {
        public Guid Id { get; init; }

        public string Item { get; set; }

        public string Description { get; set; }

        public Decimal UnitPrice { get; set; }

        public Decimal Amount { get; set; }

        public int Quantity { get; set; }

        public Guid InvoiceId { get; set; }

        public InvoiceDto Invoice { get; set; }
    }
}
