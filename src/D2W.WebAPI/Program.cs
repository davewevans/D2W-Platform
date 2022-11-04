using D2W.Application.Common.Managers;
using FluentValidation;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

// Serilog
// ref: https://code-maze.com/structured-logging-in-asp-net-core-with-serilog/

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.File(new JsonFormatter(), "log.txt")
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .CreateLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(new JsonFormatter())
        .WriteTo.Seq("http://localhost:5341")
        .WriteTo.File(new JsonFormatter(), "log.txt")
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning));

    // Add services to the container.

    builder.Services.AddApplication();

    builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

    builder.Services.AddHealthChecks();

    builder.Services.AddAppLocalization();

    builder.Services.AddAuth(builder.Configuration);

    builder.Services.AddControllers(options => { options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });

    builder.Services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters()
                    .AddValidatorsFromAssemblyContaining<IApplicationDbContext>();

    builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

    builder.Services.Configure<FormOptions>(x =>
    {
        x.ValueLengthLimit = 1073741824;
        x.MultipartBodyLengthLimit = 1073741824; // In case of multipart
    });

    builder.Services.AddSwaggerApi();

    //builder.Services.AddHttpsRedirection(options =>
    //{
    // options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    // options.HttpsPort = 44388;
    //});

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder.AllowAnyOrigin()
                                                                      .AllowAnyMethod()
                                                                      .AllowAnyHeader());
    });

    builder.Services.AddSignalR();

    builder.Services.AddSingleton<TimerManager>();

    builder.Services.AddScoped<IHubNotificationService, HubNotificationService>();

    builder.Services.AddScoped<ISignalRContextProvider, SignalRContextProvider>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            //var context = services.GetRequiredService<ApplicationDbContext>();
            //await context.Database.EnsureCreatedAsync();

            var permissionScannerService = services.GetRequiredService<IPermissionScannerService>();
            await ApplicationDbContextSeeder.SeedAsync(permissionScannerService);

            var roleManager = services.GetRequiredService<ApplicationRoleManager>();
            await ApplicationDbContextSeeder.SeedStaticRoles(roleManager);

            var dbContext = services.GetRequiredService<IApplicationDbContext>();
            await ApplicationDbContextSeeder.SeedCountries(dbContext);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogError(ex, $"An error occurred while migrating or seeding the database.| {ex.InnerException?.ToString() ?? ex.Message}");
        }
    }

    ServiceActivator.Configure(app.Services.CreateScope().ServiceProvider);

    app.UseHealthChecks("/health");

    //app.UseHttpsRedirection();

    app.UseDefaultFiles();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseCors("CorsPolicy");

    app.UseAppLocalization();

    app.UseSwaggerApi();

    app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
    {
        IsApiOnly = false,
        UseApiProblemDetailsException = true,
        ShowStatusCode = true,
        WrapWhenApiPathStartsWith = "/api",
        ExcludePaths = new[]
        {
        new AutoWrapperExcludePath("/DashboardHub/.*|/DashboardHub", ExcludeMode.Regex)
    }
    });

    app.UseTenantInterceptor(options =>
    {
        options.TenantMode = (TenantMode)Enum.Parse(typeof(TenantMode),
            builder.Configuration.GetValue<string>("AppOptions:TenantModeOptions"));
    });

    app.UseIdentityOptions();

    app.UseAuth();

    app.UseHangfireDashboard(); // If you want to access the hangfire dashboard from outside the localhost, please refer to this link. https://docs.hangfire.io/en/latest/configuration/using-dashboard.html

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<DashboardHub>("Hubs/DashboardHub");
        endpoints.MapHub<DataExportHub>("Hubs/DataExportHub");
    });

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



