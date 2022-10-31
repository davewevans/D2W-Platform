namespace D2W.WebPortal.DTOs
{
    public class SubscriptionDto
    {
        public Guid Id { get; init; }

        public string SubscriptionName { get; set; }

        // public BillingCycle BillingCycle { get; set; }

        public DateTime SubscriptionStartedDate { get; set; }

        public DateTime SubscriptionEndedDate { get; set; }

        public DateTime SubscriptionPausedDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime RenewalDate { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Paused { get; set; }

        public Guid? SubscriptionPlanId { get; set; }

        public Guid DesignerId { get; set; }

        public DesignerDto Designer { get; set; }
    }
}
