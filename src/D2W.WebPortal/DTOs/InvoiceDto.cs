namespace D2W.WebPortal.DTOs
{
    public class InvoiceDto
    {
        public Guid Id { get; init; }

        public int InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public Decimal Total { get; set; }

    
        public Decimal SubTotal { get; set; }

      
        public Decimal BalanceDue { get; set; }

     
        public Decimal AmountPaid { get; set; }
     
        public Decimal Tax { get; set; }
    
        public Decimal Discount { get; set; }

        public int GracePeriodInDays { get; set; }

        public string BillingState { get; set; }

        public string PostalCode { get; set; }

        public string BillingCountry { get; set; }

        public string Notes { get; set; }
     
        public InvoiceStatus Status { get; set; }

        public Guid SubscriptionId { get; set; }

        public SubscriptionDto Subscription { get; set; }
    
        public Guid? DesignerId { get; set; }

        public DesignerDto Designer { get; set; }

        public List<InvoiceItemDto> InvoiceItems { get; set; }

    }
}
