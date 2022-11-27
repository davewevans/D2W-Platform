using CurrieTechnologies.Razor.SweetAlert2;
using Serilog;
using Syncfusion.Blazor;

namespace D2W.WebPortal;

public class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {

        // TODO serilog set up
        // Can we send logs to Azure app insights????
        // ref: https://stackoverflow.com/questions/71220619/use-serilog-as-logging-provider-in-blazor-webassembly-client-app?rq=1

        //Log.Logger = new LoggerConfiguration()
        //    .WriteTo.
        //    .CreateLogger();

        //Log.Information("Starting web host");

        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");

        Syncfusion.Licensing.SyncfusionLicenseProvider
            .RegisterLicense(builder.Configuration["SyncfusionLicenseKey"]);

        // Syncfusion
        builder.Services.AddSyncfusionBlazor();

        builder.Services.AddScoped<SpinnerService>();

        builder.Services.AddHttpClientInterceptor();

        builder.Services.AddScoped<HttpInterceptorService>();

        builder.Services.AddScoped<IApiUrlProvider, ApiUrlProvider>();

        builder.Services.AddHttpClient("HttpInterceptorService");

        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.Configuration.GetSection("BaseApiUrl").Value)
        }.EnableIntercept(sp));

        builder.Services.AddSweetAlert2(options =>
        {
            options.Theme = SweetAlertTheme.Dark;
        });

        ConfigureServices(builder.Services);

        var host = builder.Build();

        var localStorage = host.Services.GetRequiredService<ILocalStorageService>();

        var culture = await localStorage.GetItemAsync<string>("Culture");

        var selectedCulture = culture == null ? new CultureInfo("en-US") : new CultureInfo(culture);

        //var selectedCulture = culture; You can uncomment this line and comment the above line.

        CultureInfo.DefaultThreadCurrentCulture = selectedCulture;

        CultureInfo.DefaultThreadCurrentUICulture = selectedCulture;

        await host.RunAsync();
    }

    #endregion Public Methods

    #region Private Methods

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();

        services.AddSingleton<ILocalizationService, LocalizationService>();

        services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<AuthStateProvider>();

        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<IReturnUrlProvider, ReturnUrlProvider>();

        services.AddSingleton<IAppStateManager, AppStateManager>();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<IBreadcrumbService, BreadcrumbService>();

        services.AddScoped<IHttpService, HttpService>();

        services.AddScoped<IAccountsClient, AccountsClient>();

        services.AddScoped<IManageClient, ManageClient>();

        services.AddScoped<IRolesClient, RolesClient>();

        services.AddScoped<IPermissionsClient, PermissionsClient>();

        services.AddScoped<IUsersClient, UsersClient>();

        services.AddScoped<IAppSettingsClient, AppSettingsClient>();

        services.AddScoped<IApplicantsClient, ApplicantsClient>();

        services.AddScoped<IReportsClient, ReportsClient>();

        services.AddScoped<IDashboardClient, DashboardClient>();


        services.AddScoped<IClientsClient, ClientsClient>();

        services.AddScoped<IWorkroomsClient, WorkroomsClient>();

        services.AddScoped<IDesignConceptsClient, DesignConceptsClient>();

        services.AddScoped<IFabricsClient, FabricsClient>();

        services.AddLocalization();

        services.AddBlazoredLocalStorage();

        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            config.SnackbarConfiguration.PreventDuplicates = true;
            config.SnackbarConfiguration.NewestOnTop = true;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 6000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        services.AddAuthorizationCore();
    }

    #endregion Private Methods
}