namespace D2W.Application.Common.Interfaces.UseCases.Identity;

public interface IRoleUseCase
{
    #region Public Methods

    Task<Envelope<RoleForEdit>> GetRole(GetRoleForEditQuery request);

    Task<Envelope<RolesResponse>> GetRoles(GetRolesQuery request);

    Task<Envelope<CreateRoleResponse>> AddRole(CreateRoleCommand request);

    Task<Envelope<string>> EditRole(UpdateRoleCommand request);

    Task<Envelope<string>> DeleteRole(DeleteRoleCommand request);

    Task<Envelope<RolePermissionsForEdit>> GetRolePermissions(GetRolePermissionsForEditQuery request);

    #endregion Public Methods
}