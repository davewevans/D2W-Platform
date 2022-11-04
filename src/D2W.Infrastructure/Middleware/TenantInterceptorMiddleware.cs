namespace D2W.Infrastructure.Middleware;

public static class TenantInterceptorMiddlewareExtensions
{
    #region Public Methods

    public static IApplicationBuilder UseTenantInterceptor(this IApplicationBuilder builder,
        Action<MultiTenancyOptions> configureOptions)
    {
        var options = new MultiTenancyOptions();

        configureOptions(options);

        return builder.UseMiddleware<TenantInterceptorMiddleware>(options);
        //return builder.UseWhen(_ => options.TenantMode == TenantMode.MultiTenant, appBuilder => appBuilder.UseMiddleware<TenantHandlerMiddleware>(options));
    }

    #endregion Public Methods
}

public class TenantInterceptorMiddleware
{
    #region Private Fields

    private readonly RequestDelegate _next;

    private readonly MultiTenancyOptions _options;

    #endregion Private Fields

    #region Public Constructors

    public TenantInterceptorMiddleware(RequestDelegate next, MultiTenancyOptions options)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next), nameof(next) + " is required");
        _options = options;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task InvokeAsync(HttpContext httpContext, IApplicationDbContext dbContext, ITenantResolver tenantResolver)
    {
        tenantResolver.TenantMode = _options.TenantMode;

        switch (_options.TenantMode)
        {
            case TenantMode.MultiTenant when httpContext == null:
                throw new ArgumentNullException(nameof(httpContext), nameof(httpContext) + " is required");

            case TenantMode.MultiTenant:
                {
                    // If 'callback' in path, then request is from a webhook.
                    // In webhook callbacks, tenants are set in the webhook services.
                    if (httpContext.Request.Path.Value != null &&
                        httpContext.Request.Path.Value.Contains("callback")) break;

                    // If registration workflow, then no tenant exists yet.
                    // Tenant created before app user in registration workflow.
                    if (httpContext.Request.Path.Value != null &&
                        httpContext.Request.Path.Value.ToLower().Contains("account/register")) break;


                    // If login workflow, then tenant not yet known
                    if (httpContext.Request.Path.Value != null &&
                        httpContext.Request.Path.Value.ToLower().Contains("account/login")) break;

                    // If test endpoint, ignore tenant
                    if (httpContext.Request.Path.Value != null &&
                        httpContext.Request.Path.Value.ToLower().Contains("api/test")) break;


                    var xTenantHeader = httpContext.Request.Headers["X-Tenant"];

                    if (!xTenantHeader.Any())
                    {
                        tenantResolver.SetTenantId(null);
                        tenantResolver.SetTenantName(null);
                        tenantResolver.IsHost = true;
                    }
                    else
                    {
                        var tenantName = xTenantHeader;

                        if (tenantName.Count == 0)
                            tenantName = string.Empty;

                        string tenantNameValue = tenantName.FirstOrDefault();
                        
                        var tenant = dbContext.Tenants.FirstOrDefault(t => t.Name.Equals(tenantNameValue));

                        var tenantId = tenant?.Id;

                        if (httpContext.Request.Path.Value is { } pathValue
                            && tenantId is null
                            && !pathValue.Contains("hangfire")
                            && !pathValue.Contains("/Hubs/")
                            && !pathValue.Contains("callback")
                            && !pathValue.ToLower().Contains("account/register"))
                            throw new Exception(Resource.Invalid_tenant_name);

                        tenantResolver.SetTenantId(tenantId);

                        tenantResolver.SetTenantName(tenantName);
                    }

                    break;
                }

            case TenantMode.SingleTenant:
                {
                    tenantResolver.SetTenantId(Guid.Empty);

                    tenantResolver.SetTenantName("Default");

                    break;
                }
        }

        await _next(httpContext);
    }

    #endregion Public Methods
}