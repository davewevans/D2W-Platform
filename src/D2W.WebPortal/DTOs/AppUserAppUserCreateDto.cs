namespace D2W.WebPortal.DTOs
{
    public class AppUserAppUserCreateDto
    {
        public Guid AppUserParentId { get; set; }

        public Guid AppUserChildId { get; set; }

        public bool Active { get; set; }
    }
}
