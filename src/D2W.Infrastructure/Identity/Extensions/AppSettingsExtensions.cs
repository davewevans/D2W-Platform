namespace D2W.Infrastructure.Identity.Extensions;

public static class AppSettingsExtensions
{
    #region Public Methods

    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppOptions>(configuration.GetSection(AppOptions.Section));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Section));
        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.Section));
        services.Configure<SmsOptions>(configuration.GetSection(SmsOptions.Section));
        services.Configure<ClientAppOptions>(configuration.GetSection(ClientAppOptions.Section));
        return services;
    }

    #endregion Public Methods
}