namespace D2W.HostApp.Consumers;

public class TenantsClient : ITenantsClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public TenantsClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> CreateTenant(CreateTenantCommand request)
    {
        return await _httpService.Post<CreateTenantCommand, CreateTenantResponse>("api/tenants/createTenant", request);
    }

    #endregion Public Methods
}