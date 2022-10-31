namespace D2W.WebPortal.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public bool Active { get; set; }

        public ProfileDto Profile { get; set; }
        public List<DesignConceptDto> ClientDesignConcepts { get; set; }
    }
}
