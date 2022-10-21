namespace D2W.WebPortal.Interfaces.Consumers;

public interface IRolesClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetRole(GetRoleForEditQuery request);

    Task<HttpResponseWrapper<object>> GetRolePermissions(GetRolePermissionsForEditQuery request);

    Task<HttpResponseWrapper<object>> GetRoles(GetRolesQuery request);

    Task<HttpResponseWrapper<object>> CreateRole(CreateRoleCommand request);

    Task<HttpResponseWrapper<object>> UpdateRole(UpdateRoleCommand request);

    Task<HttpResponseWrapper<object>> DeleteRole(string id);

    #endregion Public Methods
}