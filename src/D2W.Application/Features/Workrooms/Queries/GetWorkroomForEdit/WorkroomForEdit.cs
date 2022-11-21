namespace D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;

public class WorkroomForEdit : AuditableDto
{
    public WorkroomForEdit()
    {
        Countries = new List<CountryItem>();
    }

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
    public Guid? CountryId { get; set; }
    public bool IsLinkedToAnotherTenant { get; set; }
    public List<CountryItem> Countries { get; set; }
    

    #endregion Public Properties

    #region Public Methods

    public static WorkroomForEdit MapFromEntity(ApplicationUser appUser)
    {
        return new()
        {
            Id = appUser.Id,

            CreatedOn = appUser.CreatedOn,
            CreatedBy = appUser.CreatedBy,
            ModifiedOn = appUser.ModifiedOn,
            ModifiedBy = appUser.ModifiedBy,
        };
    }

    public void MapFromContactDetailsEntity(ContactDetailsModel contactDetails)
    {
        EmailAddress = contactDetails.EmailAddress;
        PhoneNumber = contactDetails.PhoneNumber;
        CompanyName = contactDetails.CompanyName;
        AltEmailAddress = contactDetails.AltEmailAddress;
        AltPhoneNumber = contactDetails.AltPhoneNumber;
        Fax = contactDetails.Fax;
        AddressLine1 = contactDetails.AddressLine1;
        AddressLine2 = contactDetails.AddressLine2;
        City = contactDetails.City;
        Region = contactDetails.Region;
        PostalCode = contactDetails.PostalCode;
        CountryId = contactDetails.CountryId;
    }

    public void MapFromCountryEntity(List<CountryModel> countries)
    {
        Countries = countries.Select(x => new CountryItem
        {
            Id = x.Id,
            CountryCode = x.CountryCode,
            CountryName = x.CountryName,
            CountryFlagUri = x.CountryFlagUri
        }).ToList();
    }

    #endregion Public Methods
}
