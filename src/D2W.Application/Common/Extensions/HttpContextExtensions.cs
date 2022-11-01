namespace D2W.Application.Common.Extensions;

public static class HttpContextExtensions
{
    #region Public Methods

    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId ?? string.Empty;
    }

    public static string GetUserName(this IHttpContextAccessor httpContextAccessor)
    {
        var userManager = httpContextAccessor.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();

        if (userManager == null)
            throw new ArgumentException(nameof(userManager));

        var userName = userManager.GetUserName(httpContextAccessor.HttpContext.User);

        return userName;
    }

    public static string GetLanguage(this IHttpContextAccessor httpContextAccessor)
    {
        var language = httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

        return language;
    }

    public static string GetTenantFromRequestHeader(this IHttpContextAccessor httpContextAccessor)
    {
        var tenantName = httpContextAccessor.HttpContext.Request.Headers["X-Tenant"];

        return tenantName.Count == 0 ? string.Empty : tenantName;
    }

    public static string GetClientAppHostName(this IHttpContextAccessor httpContextAccessor)
    {
        var configReaderService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfigReaderService>();
        var tenantResolver = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ITenantResolver>();
        var clientAppOptions = configReaderService.GetClientAppOptions();

        return tenantResolver.TenantMode switch
        {
            TenantMode.MultiTenant => tenantResolver.IsHost ? clientAppOptions.SingleTenantHostName
                : string.Format(clientAppOptions.MultiTenantHostName, configReaderService.GetSubDomain()),
            _ => clientAppOptions.SingleTenantHostName
        };
    }

    public static string GetClientAppHostNameWithoutTenant(this IHttpContextAccessor httpContextAccessor)
    {
        var configReaderService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfigReaderService>();
        var tenantResolver = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ITenantResolver>();
        var clientAppOptions = configReaderService.GetClientAppOptions();
        return clientAppOptions.MultiTenantHostName;
    }

    #endregion Public Methods
}