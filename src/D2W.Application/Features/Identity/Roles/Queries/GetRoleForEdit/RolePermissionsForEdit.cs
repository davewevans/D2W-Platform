namespace D2W.Application.Features.Identity.Roles.Queries.GetRoleForEdit;

public class RolePermissionsForEdit : AuditableDto
{
    #region Public Constructors

    public RolePermissionsForEdit()
    {
        SelectedPermissions = new List<PermissionItem>();
        RequestedPermissions = new List<PermissionItem>();
    }

    #endregion Public Constructors

    #region Public Properties

    public string RoleId { get; set; }
    public IList<PermissionItem> RequestedPermissions { get; set; }
    public IList<PermissionItem> SelectedPermissions { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static RolePermissionsForEdit MapFromEntity(ApplicationRole role, List<PermissionItem> selectedPermissions, List<PermissionItem> requestedPermissions)
    {
        return new()
        {
            RoleId = role.Id,
            SelectedPermissions = selectedPermissions,
            RequestedPermissions = requestedPermissions,
        };
    }

    #endregion Public Methods
}