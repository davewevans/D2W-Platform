namespace D2W.Application.Features.Workrooms.Queries.GetWorkrooms;

public class WorkroomItem : AuditableDto
{
    #region Public Properties

    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string EmailAddress { get; set; }
    public string AltEmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string AltPhoneNumber { get; set; }
    public string Fax { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static WorkroomItem MapFromEntity(ApplicationUser appUser, Guid tenantId)
    {
        var contactDetails = appUser.ContactDetails.FirstOrDefault(c => c.TenantId.Equals(tenantId));

        return new()
        {
            Id = appUser.Id,
            EmailAddress = appUser.Email,
            PhoneNumber = appUser.PhoneNumber,

            // Contact details
            CompanyName = contactDetails?.CompanyName,
            AltEmailAddress = contactDetails?.AltEmailAddress,
            AltPhoneNumber = contactDetails?.AltPhoneNumber,
            Fax = contactDetails?.Fax,
            AddressLine1 = contactDetails?.AddressLine1,
            AddressLine2 = contactDetails?.AddressLine2,
            City = contactDetails?.City,
            Region = contactDetails?.Region,
            PostalCode = contactDetails?.PostalCode,

            CreatedOn = appUser.CreatedOn,
            CreatedBy = appUser.CreatedBy,
            ModifiedOn = appUser.ModifiedOn,
            ModifiedBy = appUser.ModifiedBy,
        };
    }

    #endregion Public Methods
}
