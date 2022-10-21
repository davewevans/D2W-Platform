namespace D2W.Application.Features.Identity.Roles.Queries.GetRoleForEdit;

public class RoleForEdit : AuditableDto
{
    #region Public Properties

    public string Id { get; set; }
    public bool IsDefault { get; set; }
    public string Name { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static RoleForEdit MapFromEntity(ApplicationRole role)
    {
        return new RoleForEdit
        {
            Id = role.Id,
            Name = role.Name,
            IsDefault = role.IsDefault,
            CreatedOn = role.CreatedOn,
            CreatedBy = role.CreatedBy,
            ModifiedOn = role.ModifiedOn,
            ModifiedBy = role.ModifiedBy
        };
    }

    #endregion Public Methods
}