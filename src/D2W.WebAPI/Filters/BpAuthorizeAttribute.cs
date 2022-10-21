namespace D2W.WebAPI.Filters;

[AttributeUsage(AttributeTargets.Class)]
public class BpAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    #region Public Methods

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContextAccessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();
        var routeData = (httpContextAccessor.HttpContext ?? throw new InvalidOperationException()).GetRouteData();

        //var areaName = routeData?.Values["area"]?.ToString();

        var username = context.HttpContext.User.Identity?.Name;

        var isSuperAdmin = (await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username))?.IsSuperAdmin;

        if (isSuperAdmin.HasValue && isSuperAdmin.Value)
            return;

        var controllerName = routeData.Values["controller"]?.ToString();

        var actionName = routeData.Values["action"]?.ToString();

        var permission = $"{controllerName}.{actionName}";

        var allowAnonymous = !await dbContext.ApplicationPermissions.AnyAsync(p => p.Name == permission);

        if (allowAnonymous)
            return;

        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
            throw new ApiProblemDetailsException(string.Format(Resource.You_are_not_authorized, context.HttpContext.Request.GetDisplayUrl()), 401);

        var claim = new Claim("permissions", permission);

        if (!context.HttpContext.User.HasClaim(c => c.Type == claim.Type && c.Value == claim.Value)) //To get all inherited permission and direct permissions
            throw new ApiProblemDetailsException(string.Format(Resource.You_are_forbidden, context.HttpContext.Request.GetDisplayUrl()), 403);
    }

    #endregion Public Methods
}