namespace D2W.WebPortal.DTOs
{
    public class DesignConceptsApprovalsCreateDto
    {
        public Guid ClientId { get; set; }
        public bool IsApproved { get; set; }
        public Guid DesignConceptId { get; set; }
    }
}
