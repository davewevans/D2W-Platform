using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Clients.Queries.GetClients;

public class ClientItem : AuditableDto
{
    #region Public Properties
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ApplicationUserType AppUserType { get; set; }
    public string AvatarUri { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static ClientItem MapFromEntity(ApplicationUser appUser)
    {
        return new()
        {
            Id = appUser.Id,
            FullName = appUser.FullName,
            Email = appUser.Email,
            PhoneNumber = appUser.PhoneNumber,
            AppUserType = appUser.AppUserType,
            AvatarUri = appUser.AvatarUri,
            CreatedOn = appUser.CreatedOn,
            CreatedBy = appUser.CreatedBy,
            ModifiedOn = appUser.ModifiedOn,
            ModifiedBy = appUser.ModifiedBy
        };
    }

    #endregion Public Methods
}
