namespace D2W.Application.Common.Interfaces.UseCases.Identity;

public interface IUserUseCase
{
    #region Public Methods

    Task<Envelope<UserForEdit>> GetUser(GetUserForEditQuery request);

    Task<Envelope<UsersResponse>> GetUsers(GetUsersQuery request);

    Task<Envelope<CreateUserResponse>> AddUser(CreateUserCommand request);

    Task<Envelope<string>> EditUser(UpdateUserCommand request);

    Task<Envelope<string>> DeleteUser(DeleteUserCommand request);

    Task<Envelope<UserPermissionsResponse>> GetUserPermissions(GetUserPermissionsQuery request);

    Task<Envelope<string>> GrantOrRevokeUserPermissions(GrantOrRevokeUserPermissionsCommand request);

    Task<List<PermissionItem>> GetUserNonExcludedPermissions(ApplicationUser user);

    #endregion Public Methods
}