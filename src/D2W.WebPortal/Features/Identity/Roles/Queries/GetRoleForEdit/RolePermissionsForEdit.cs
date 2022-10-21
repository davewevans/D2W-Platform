namespace D2W.WebPortal.Features.Identity.Roles.Queries.GetRoleForEdit;

public class RolePermissionsForEdit
{
    #region Public Properties

    public bool IsDefault { get; set; }
    public string Name { get; set; }
    public IList<PermissionItem> RequestedPermissions { get; set; }
    public string RoleId { get; set; }
    public IList<PermissionItem> SelectedPermissions { get; set; }

    #endregion Public Properties
}