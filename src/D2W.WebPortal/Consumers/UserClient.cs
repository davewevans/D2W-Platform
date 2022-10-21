namespace D2W.WebPortal.Consumers;

public class UsersClient : IUsersClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public UsersClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> GetUser(GetUserForEditQuery request)
    {
        return await _httpService.Post<GetUserForEditQuery, UserForEdit>("users/GetUser", request);
    }

    public async Task<HttpResponseWrapper<object>> GetUsers(GetUsersQuery request)
    {
        return await _httpService.Post<GetUsersQuery, UsersResponse>("users/GetUsers", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateUserFormData(MultipartFormDataContent request)
    {
        return await _httpService.PostFormData<MultipartFormDataContent, CreateUserResponse>("users/CreateUser", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateUserFormData(MultipartFormDataContent request)
    {
        return await _httpService.PostFormData<MultipartFormDataContent, string>("users/UpdateUser", request);
    }

    public async Task<HttpResponseWrapper<object>> DeleteUser(string id)
    {
        return await _httpService.Delete<string>($"users/DeleteUser?id={id}");
    }

    public async Task<HttpResponseWrapper<object>> GetUserPermissions(GetUserPermissionsQuery request)
    {
        return await _httpService.Post<GetUserPermissionsQuery, UserPermissionsResponse>("users/GetUserPermissions", request);
    }

    public async Task<HttpResponseWrapper<object>> GrantOrRevokeUserPermissions(GrantOrRevokeUserPermissionsCommand request)
    {
        return await _httpService.Post<GrantOrRevokeUserPermissionsCommand, string>("users/GrantOrRevokeUserPermissions", request);
    }

    #endregion Public Methods
}