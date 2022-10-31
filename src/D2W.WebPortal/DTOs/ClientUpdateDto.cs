namespace D2W.WebPortal.DTOs
{
    public class ClientUpdateDto
    {
        public string UserName { get; init; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public ProfileDto Profile { get; set; }
    }
}
