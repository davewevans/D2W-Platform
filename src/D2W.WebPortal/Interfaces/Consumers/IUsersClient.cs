namespace D2W.WebPortal.Interfaces.Consumers;

public interface IUsersClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> CreateUserFormData(MultipartFormDataContent request);

    Task<HttpResponseWrapper<object>> DeleteUser(string Id);

    Task<HttpResponseWrapper<object>> GetUser(GetUserForEditQuery request);

    Task<HttpResponseWrapper<object>> GetUsers(GetUsersQuery request);

    Task<HttpResponseWrapper<object>> UpdateUserFormData(MultipartFormDataContent request);

    Task<HttpResponseWrapper<object>> GetUserPermissions(GetUserPermissionsQuery request);

    Task<HttpResponseWrapper<object>> GrantOrRevokeUserPermissions(GrantOrRevokeUserPermissionsCommand request);

    #endregion Public Methods
}