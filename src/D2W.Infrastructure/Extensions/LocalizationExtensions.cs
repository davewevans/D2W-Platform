namespace D2W.Infrastructure.Extensions;

public static class LocalizationExtensions
{
    #region Public Methods

    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {
        services.AddLocalization(o => o.ResourcesPath = "Resource");
        services.AddSingleton<ILocalizationService, LocalizationService>();
        return services;
    }

    public static IApplicationBuilder UseAppLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = new[]
        {
            "en-US",
            "ar-SA",
            "de-DE",
            "es-ES",
            "fr-FR",
            "hi-IN",
            "ja-JP",
            "pt-PT",
            "tr-TR",
            "zh-CN"
        };

        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        localizationOptions.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
        {
            var userLanguages = context.Request.Headers["Accept-Language"].ToString();
            var firstLanguage = userLanguages.Split(',').FirstOrDefault();
            var defaultLanguage = string.IsNullOrEmpty(firstLanguage) ? supportedCultures[0] : firstLanguage;
            return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
        }));

        app.UseRequestLocalization(localizationOptions);
        return app;
    }

    #endregion Public Methods
}