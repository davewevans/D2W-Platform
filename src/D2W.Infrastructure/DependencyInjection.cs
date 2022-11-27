using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Common.Managers;
using D2W.Application.UseCases;

namespace D2W.Infrastructure;

public static class DependencyInjection
{
    #region Public Methods

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        //string connectionStringName = environment.EnvironmentName switch
        //{
        //    "Development" => "DefaultConnection",
        //    "Production" => "SqlServerConnectionProduction",
        //    "Staging" => "SqlServerConnectionStaging",
        //    _ => ""
        //};

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddHangfire(globalConfiguration => globalConfiguration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        services.AddHangfireServer();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddPasswordValidator<CustomPasswordValidator<ApplicationUser>>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            //.AddUserStore<CustomUserStore>()
            //.AddRoleStore<CustomRoleStore>()
            ;

        services.Replace(ServiceDescriptor.Scoped<IUserValidator<ApplicationUser>, MultiTenantUserValidator>());
        services.Replace(ServiceDescriptor.Scoped<IRoleValidator<ApplicationRole>, MultiTenantRoleValidator>());

        #region AlterDefaultPasswordHashingMethod

        //services.Configure<PasswordHasherOptions>(options =>
        //{
        //    options.IterationCount = 10000;
        //});

        #endregion AlterDefaultPasswordHashingMethod

        #region AnotherPasswordHashingMethod

        //services.AddScoped<IPasswordHasher<Client>, BCryptPasswordHasher<Client>>();
        //services.Configure<BCryptPasswordHasherOptions>(options =>
        //{
        //    options.WorkFactor = 10;
        //    options.EnhancedEntropy = false;
        //});

        #endregion AnotherPasswordHashingMethod

        // X-CSRF-Token
        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-XSRF-Token";
            options.SuppressXFrameOptionsHeader = false;
        });

        services.AddHttpContextAccessor();
        services.AddAppSettings(configuration);

        services.AddScoped<IdentityErrorDescriber, LocalizedIdentityErrorDescriber>();
        services.AddScoped<IStorageProvider, StorageProvider>();
        services.AddScoped<IStorageFactory, StorageFactory>();
        services.AddScoped<ITenantResolver, TenantResolver>();
        services.AddScoped<IDemoIdentitySeeder, DemoIdentitySeeder>();
        services.AddScoped<IReportingService, ReportingService>();
        services.AddScoped<IHtmlReportBuilder, HtmlReportBuilder>();

        services.AddScoped<IFileStorageService, AzureStorageService>();
        services.AddScoped<IFileStorageService, OnPremiseStorageService>();
        services.AddScoped<IDataExportService, DataExportService>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<IConfigReaderService, ConfigReaderService>();
        services.AddScoped<IPermissionScannerService, PermissionScannerService>();
        services.AddScoped<IAppSettingsUseCase, AppSettingsUseCase>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IDemoUserPasswordSetterService, DemoUserPasswordSetterService>();

        services.AddScoped<IAccountUseCase, AccountUseCase>();
        services.AddScoped<IManageUseCase, ManageUseCase>();
        services.AddScoped<IRoleUseCase, RoleUseCase>();
        services.AddScoped<IPermissionUseCase, PermissionUseCase>();
        services.AddScoped<IUserUseCase, UserUseCase>();
        services.AddScoped<ITenantUseCase, TenantUseCase>();
        services.AddScoped<IApplicantUseCase, ApplicantUseCase>();
        services.AddScoped<IReportUseCase, ReportUseCase>();

        services.AddScoped<IClientUseCase, ClientUseCase>();
        services.AddScoped<IWorkroomUseCase, WorkroomUseCase>();
        services.AddScoped<IDesignConceptUseCase, DesignConceptUseCase>();
        services.AddScoped<IFabricUseCase, FabricUseCase>();



        return services;
    }

    #endregion Public Methods
}