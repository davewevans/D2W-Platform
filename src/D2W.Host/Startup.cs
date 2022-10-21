namespace D2W.HostApp;

public class Startup
{
    #region Public Constructors

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion Public Constructors

    #region Public Properties

    public IConfiguration Configuration { get; }

    #endregion Public Properties

    #region Public Methods

    // This method gets called by the runtime. Use this method to add services to the container. For
    // more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddMudServices();
        services.AddScoped<ITenantUrlProvider, TenantUrlProvider>();

        services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(Configuration.GetSection("UrlOptions:BaseApiUrl").Value)
        });

        services.AddScoped<IHttpService, HttpService>();
        services.AddScoped<ITenantsClient, TenantsClient>();
        services.Configure<UrlOptions>(Configuration.GetSection(UrlOptions.Section));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production
            // scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }

    #endregion Public Methods
}