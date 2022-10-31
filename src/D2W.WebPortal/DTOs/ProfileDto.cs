namespace D2W.WebPortal.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhonePrimary { get; set; } = string.Empty;

        public string PhoneSecondary { get; set; } = string.Empty;

        public string StreetAddress1 { get; set; } = string.Empty;

        public string StreetAddress2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;

        public string WorkroomName { get; set; } = string.Empty;

        public string ContactNamePrimary { get; set; } = string.Empty;

        public string ContactNameSecondary { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;

        public Guid AppUserId { get; set; }
    }
}
