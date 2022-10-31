namespace D2W.WebPortal.DTOs
{
    public class EmailDto
    {
        public Guid Id { get; init; }

        public Guid? ClientId { get; set; }

        public Guid? WorkroomId { get; set; }
        
        public string ToEmailAddress { get; set; }

        public string FromEmailAddress { get; set; }

        public string Subject { get; set; }
        public string HtmlBody { get; set; }

        public string Body { get; set; }

        public EmailStatus Status { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime DateReceived { get; set; }

        public Guid? DesignerId { get; set; }

        public DesignerDto Designer { get; set; }
    }
}
