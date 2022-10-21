namespace D2W.WebPortal.Consumers;

public class RolesClient : IRolesClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public RolesClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> GetRole(GetRoleForEditQuery request)
    {
        return await _httpService.Post<GetRoleForEditQuery, RoleForEdit>("roles/GetRole", request);
    }

    public async Task<HttpResponseWrapper<object>> GetRolePermissions(GetRolePermissionsForEditQuery request)
    {
        return await _httpService.Post<GetRolePermissionsForEditQuery, RolePermissionsForEdit>("roles/GetRolePermissions", request);
    }

    public async Task<HttpResponseWrapper<object>> GetRoles(GetRolesQuery request)
    {
        return await _httpService.Post<GetRolesQuery, RolesResponse>("roles/GetRoles", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateRole(CreateRoleCommand request)
    {
        return await _httpService.Post<CreateRoleCommand, CreateRoleResponse>("roles/CreateRole", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateRole(UpdateRoleCommand request)
    {
        return await _httpService.Put<UpdateRoleCommand, string>("roles/UpdateRole", request);
    }

    public async Task<HttpResponseWrapper<object>> DeleteRole(string id)
    {
        return await _httpService.Delete<string>($"roles/DeleteRole?id={id}");
    }

    #endregion Public Methods
}