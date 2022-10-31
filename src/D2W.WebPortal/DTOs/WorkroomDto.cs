namespace D2W.WebPortal.DTOs
{
    public class WorkroomDto
    {
        public Guid Id { get; init; }

        public string UserName { get; init; }
        public string Password { get; set; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }

        public bool Active { get; set; }

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public ProfileDto Profile { get; set; }
    }
}
