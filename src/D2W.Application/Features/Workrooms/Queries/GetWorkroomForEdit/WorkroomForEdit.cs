namespace D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;

public class WorkroomForEdit : AuditableDto
{
    #region Public Properties
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AltEmailAddress { get; set; }
    public string AltPhone1 { get; set; }
    public string ContactName1 { get; set; }
    public string ContactName2 { get; set; }
    public string AvatarUri { get; set; }

    public string AppUserType { get; set; }

    // TODO address

    #endregion Public Properties

    #region Public Methods

    public static WorkroomForEdit MapFromEntity(ApplicationUser appUser)
    {
        return new()
        {
            Id = appUser.Id,
            Email = appUser.Email,
            PhoneNumber = appUser.PhoneNumber,
            AppUserType = appUser.AppUserType.ToString(),
            AvatarUri = appUser.AvatarUri,
            CreatedOn = appUser.CreatedOn,
            CreatedBy = appUser.CreatedBy,
            ModifiedOn = appUser.ModifiedOn,
            ModifiedBy = appUser.ModifiedBy
        };
    }

    #endregion Public Methods
}
