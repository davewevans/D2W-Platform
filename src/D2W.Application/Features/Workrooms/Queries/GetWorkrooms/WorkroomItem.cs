﻿namespace D2W.Application.Features.Workrooms.Queries.GetWorkrooms;

public class WorkroomItem : AuditableDto
{
    // TODO contact details

    #region Public Properties

    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AltEmailAddress { get; set; }
    public string AltPhoneNumber { get; set; }
    public string Fax { get; set; }
 
  
    public ApplicationUserType AppUserType { get; set; }
    public string AvatarUri { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static WorkroomItem MapFromEntity(ApplicationUser appUser, Guid tenantId)
    {
        var contactDetails = appUser.ContactDetails.FirstOrDefault(c => c.TenantId.Equals(tenantId));

        return new()
        {
            Id = appUser.Id,
            Email = appUser.Email,
            PhoneNumber = appUser.PhoneNumber,
            AppUserType = appUser.AppUserType,
            AvatarUri = appUser.AvatarUri,
            CreatedOn = appUser.CreatedOn,
            CreatedBy = appUser.CreatedBy,
            ModifiedOn = appUser.ModifiedOn,
            ModifiedBy = appUser.ModifiedBy,

            // Contact details
            CompanyName = contactDetails?.CompanyName,
            AltEmailAddress = contactDetails?.AltEmailAddress,
            AltPhoneNumber = contactDetails?.AltPhoneNumber,
            Fax = contactDetails?.Fax,
        };
    }

    #endregion Public Methods
}
