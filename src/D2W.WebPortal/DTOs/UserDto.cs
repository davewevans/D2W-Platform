namespace D2W.WebPortal.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; } = string.Empty;

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public ProfileDto Profile { get; set; } = null!;
    }
}
