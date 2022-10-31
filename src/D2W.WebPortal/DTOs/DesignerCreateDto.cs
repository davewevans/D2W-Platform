namespace D2W.WebPortal.DTOs
{
    public class DesignerCreateDto
    {
        public string UserName { get; set; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }

        // Admin, Designer, Client, Workroom
        public string UserRole { get; set; }

        public AppUserRole AppUserType { get; set; }

        public ProfileDto Profile { get; set; }
    }
}
