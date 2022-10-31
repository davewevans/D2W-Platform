namespace D2W.WebPortal.DTOs
{
    public class ClientDesignDto
    {
        public Guid Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? ClientId { get; set; }

        public Guid? DesignerId { get; set; }

        public string ImageUrl { get; set; }

        public bool IsArchived { get; set; }
    }
}