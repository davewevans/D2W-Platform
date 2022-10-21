namespace D2W.WebAPI.Services.HubServices;

public class SignalRContextProvider : ISignalRContextProvider
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public SignalRContextProvider(IApplicationDbContext dbContext, ITenantResolver tenantResolver)
    {
        _dbContext = dbContext;
        _tenantResolver = tenantResolver;
    }

    #endregion Public Constructors

    #region Public Methods

    public string GetHostName(HubCallerContext hubCallerContext)
    {
        ThrowExceptionIfNull(hubCallerContext);

        var httpContext = hubCallerContext.GetHttpContext()?.Request;

        return $"{httpContext?.Scheme}://{httpContext?.Host}";
    }

    public Guid? GetTenantId(HubCallerContext hubCallerContext)
    {
        return _dbContext.Tenants.FirstOrDefault(t => t.Name == GetTenantName(hubCallerContext))?.Id;
    }

    public string GetTenantName(HubCallerContext hubCallerContext)
    {
        ThrowExceptionIfNull(hubCallerContext);

        return hubCallerContext.GetHttpContext()?.Request.Query["X-Tenant"].ToString();
    }

    public string GetUserName(HubCallerContext hubCallerContext)
    {
        ThrowExceptionIfNull(hubCallerContext);

        if (!hubCallerContext.User.IsAuthenticated())
            throw new Exception(Resource.You_are_not_authorized);

        return hubCallerContext.User?.Identity?.Name?.Split("@")[0];
    }

    public string GetUserNameIdentifier(HubCallerContext hubCallerContext)
    {
        ThrowExceptionIfNull(hubCallerContext);

        return hubCallerContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public void SetTenantIdViaTenantResolver(HubCallerContext hubCallerContext)
    {
        ThrowExceptionIfNull(hubCallerContext);

        var tenantId = GetTenantId(hubCallerContext);

        _tenantResolver.SetTenantId(tenantId);
    }

    #endregion Public Methods

    #region Private Methods

    private void ThrowExceptionIfNull(HubCallerContext hubCallerContext)
    {
        if (hubCallerContext is null)
            throw new ArgumentNullException(nameof(hubCallerContext));
    }

    #endregion Private Methods
}