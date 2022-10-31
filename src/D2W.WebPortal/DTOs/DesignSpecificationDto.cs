namespace D2W.WebPortal.DTOs
{
    public class DesignSpecificationDto
    {
        public Guid Id { get; init; }

        public Guid DesignerId { get; set; }

        public DesignerDto Designer { get; set; }
    }
}
