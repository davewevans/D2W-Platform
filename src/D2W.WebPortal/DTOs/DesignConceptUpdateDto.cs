namespace D2W.WebPortal.DTOs
{
    public class DesignConceptUpdate
    {
        public string ImageUrl { get; set; }

        public Guid DesignerId { get; set; }

        public Guid ClientId { get; set; }

        public bool IsArchived { get; set; }

        public bool IsApproved { get; set; }
    }
}
