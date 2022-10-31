namespace D2W.WebPortal.DTOs
{
    public class WorkroomCreateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }

        // Admin, Designer, Client, Workroom
        public string UserRole { get; set; }

        public Guid? DesignerId { get; set; }

        public AppUserRole AppUserType { get; set; }

        public ProfileDto Profile { get; set; }

        public bool InvitationAccepted { get; set; }

        public AppUserAppUserCreateDto AppUserAppUser { get; set; }
    }
}
