namespace D2W.WebPortal.DTOs
{
    public class DesignerDto
    {
        public Guid Id { get; init; }

        public string UserName { get; init; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public ProfileDto Profile { get; set; }

        public SubscriptionDto Subscription { get; set; }

        public List<InvoiceDto> Invoices { get; set; }

        public List<WorkOrderDto> WorkOrders { get; set; }

        public List<EmailDto> Emails { get; set; }

        public List<DesignSpecificationDto> DesignSpecifications { get; set; }


        public List<NotificationDto> Notifications { get; set; }
    }
}
