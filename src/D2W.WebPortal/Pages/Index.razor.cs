namespace D2W.WebPortal.Pages;

public partial class Index : IAsyncDisposable
{
    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDashboardClient DashboardClient { get; set; }
    [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
    [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private HubConnection HubConnection { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; } = new();
    private HeadlinesResponse HeadlinesResponse { get; set; } = new();

    #endregion Private Properties

    #region Public Methods

    public async Task InitiateSignalRHub()
    {
        var httpResponseWrapper = await DashboardClient.GetHeadlinesData();

        if (httpResponseWrapper.Response is SuccessResult<HeadlinesResponse> successResult)
            HeadlinesResponse = successResult.Result;

        if (httpResponseWrapper.Success)
        {
            await StartHubConnection();
            HubConnection.On<HeadlinesResponse>("SendHeadlinesData", (data) =>
            {
                HeadlinesResponse = data;
                StateHasChanged();
            });
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (HubConnection is not null && HubConnection.State == HubConnectionState.Connected)
        {
            try
            {
                await HubConnection.StopAsync();
            }
            finally
            {
                await HubConnection.DisposeAsync();
                Snackbar.Add("Dashboard Hub is closed.", Severity.Error);
            }
        }
    }

    public async void CloseHubConnection()
    {
        if (HubConnection is not null && HubConnection.State == HubConnectionState.Connected)
        {
            try
            {
                await HubConnection.StopAsync();
            }
            finally
            {
                await HubConnection.DisposeAsync();
                Snackbar.Add("Dashboard Hub is closed.", Severity.Error);
            }
        }
    }

    #endregion Public Methods

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Dashboard, "#", true)
        });

        await InitiateSignalRHub();
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task StartHubConnection()
    {
        Snackbar.Add("Dashboard Hub is being initialized.", Severity.Info);

        if (HubConnection is null || HubConnection.State == HubConnectionState.Disconnected)
        {
            var subDomain = NavigationManager.GetSubDomain();

            HubConnection = new HubConnectionBuilder()
                            .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/DashboardHub?X-Tenant={subDomain}&Accept-Language={CultureInfo.CurrentCulture}",
                                     options =>
                                     {
                                         //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
                                         options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
                                     }).Build();

            try
            {
                await HubConnection.StartAsync();
            }
            catch (Exception e)
            {
                Snackbar.Add($@"Unable to connect to the dashboard hub due to an error: {e.Message}", Severity.Error);
            }

            if (HubConnection.State == HubConnectionState.Connected)
                Snackbar.Add("Dashboard Hub is now connected.", Severity.Success);

            HubConnection.Closed += OnHubConnectionOnClosed;
        }
    }
    private Task OnHubConnectionOnClosed(Exception exception)
    {
        switch (exception)
        {
            case null:
                Snackbar.Add("Dashboard Hub is closed.", Severity.Error);
                break;

            default:
                Snackbar.Add($@"Dashboard Hub connection closed due to an error: {exception.Message}", Severity.Error);
                break;
        }

        return Task.CompletedTask;
    }
}

#endregion Private Methods
