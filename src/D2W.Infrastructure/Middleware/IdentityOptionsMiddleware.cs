namespace D2W.Infrastructure.Middleware;

public static class IdentityOptionsMiddlewareExtensions
{
    #region Public Methods

    public static IApplicationBuilder UseIdentityOptions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IdentityOptionsMiddleware>();
    }

    #endregion Public Methods
}

public class IdentityOptionsMiddleware
{
    #region Private Fields

    private readonly RequestDelegate _next;

    #endregion Private Fields

    #region Public Constructors

    public IdentityOptionsMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next), nameof(next) + " is required");
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task InvokeAsync(HttpContext httpContext,
        IApplicationDbContext applicationDbContext,
        IConfigReaderService configReaderService,
        IOptions<IdentityOptions> identityOptions)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext), nameof(httpContext) + " is required");

        var path = httpContext.Request.Path;

        if (path.Value != null && (path.Value.Contains("register", StringComparison.InvariantCultureIgnoreCase)
                                   || path.Value.Contains("login", StringComparison.InvariantCultureIgnoreCase)
                                   || path.Value.Contains("manage", StringComparison.InvariantCultureIgnoreCase)
                                   || path.Value.Contains("account", StringComparison.InvariantCultureIgnoreCase)
                                   || path.Value.Contains("CreateTenant", StringComparison.InvariantCultureIgnoreCase)
            ))
        {
            var userSettings = await applicationDbContext.UserSettings.FirstOrDefaultAsync() ?? configReaderService.GetAppUserOptions().MapToEntity();
            var passwordSettings = await applicationDbContext.PasswordSettings.FirstOrDefaultAsync() ?? configReaderService.GetAppPasswordOptions().MapToEntity();
            var lockoutSettings = await applicationDbContext.LockoutSettings.FirstOrDefaultAsync() ?? configReaderService.GetAppLockoutOptions().MapToEntity();
            var signInSettings = await applicationDbContext.SignInSettings.FirstOrDefaultAsync() ?? configReaderService.GetAppSignInOptions().MapToEntity();

            ApplyNewIdentityOptions(identityOptions.Value, userSettings, passwordSettings, lockoutSettings, signInSettings);
        }

        await _next(httpContext);
    }

    #endregion Public Methods

    #region Private Methods

    private static void ApplyNewIdentityOptions(IdentityOptions identityOptions, UserSettings userSettings, PasswordSettings passwordSettings, LockoutSettings lockoutSettings, SignInSettings signInSettings)
    {
        identityOptions.User.AllowedUserNameCharacters = userSettings.AllowedUserNameCharacters;
        identityOptions.User.RequireUniqueEmail = true;

        identityOptions.Password.RequiredLength = passwordSettings.RequiredLength;
        identityOptions.Password.RequiredUniqueChars = passwordSettings.RequiredUniqueChars;
        identityOptions.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
        identityOptions.Password.RequireLowercase = passwordSettings.RequireLowercase;
        identityOptions.Password.RequireUppercase = passwordSettings.RequireUppercase;
        identityOptions.Password.RequireDigit = passwordSettings.RequireDigit;

        identityOptions.Lockout.AllowedForNewUsers = lockoutSettings.AllowedForNewUsers;
        identityOptions.Lockout.MaxFailedAccessAttempts = lockoutSettings.MaxFailedAccessAttempts;
        identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettings.DefaultLockoutTimeSpan);

        //identityOptions.SignIn.RequireConfirmedEmail = signInSettings.RequireConfirmedEmail;
        //identityOptions.SignIn.RequireConfirmedPhoneNumber = signInSettings.RequireConfirmedPhoneNumber;
        identityOptions.SignIn.RequireConfirmedAccount = signInSettings.RequireConfirmedAccount;
    }

    #endregion Private Methods
}