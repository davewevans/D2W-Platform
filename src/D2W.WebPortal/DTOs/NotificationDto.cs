namespace D2W.WebPortal.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; init; }

        public Guid? ClientId { get; set; }

        public Guid? WorkroomId { get; set; }

        public string Message { get; set; }

        public Guid DesignerId { get; set; }

        public DesignerDto Designer { get; set; }
    }
}
