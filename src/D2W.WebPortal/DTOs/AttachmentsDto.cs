namespace D2W.WebPortal.DTOs
{
    public class AttachmentsDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid EmailId { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? ContentType { get; set; }
        public long? ContentLength { get; set; }
    }
}
