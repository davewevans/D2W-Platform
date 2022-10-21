namespace D2W.WebPortal.Pages.Reporting;

public partial class Reports : IAsyncDisposable
{
    #region Private Properties

    [Inject] private IAccessTokenProvider AccessTokenProvider { get; set; }
    [Inject] private IApiUrlProvider ApiUrlProvider { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IReportsClient ReportsClient { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private string SearchString { get; set; }
    private GetReportsQuery GetReportsQuery { get; set; } = new();
    private ReportForEdit ReportForEdit { get; set; } = new();
    private ReportsResponse ReportsResponse { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private MudTable<ReportItem> Table { get; set; }
    private HubConnection HubConnection { get; set; }
    private ReportStatus? SelectedReportStatus { get; set; }

    #endregion Private Properties

    #region Public Methods

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
            new(Resource.Reports, "#", true)
        });

        var userIdentity = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

        if (userIdentity is { IsAuthenticated: true })
        {
            await StartHubConnection();

            HubConnection.On("RefreshReportsViewer", async () => { await Table.ReloadServerData(); });
        }
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task DownloadReport(string reportFileName, string reportFileUri)
    {
        await Js.InvokeVoidAsync("triggerFileDownload", reportFileName, reportFileUri);
    }

    private void FilterReports(string searchString, ReportStatus? reportStatus)
    {
        SearchString = searchString;
        SelectedReportStatus = reportStatus;
        Table.ReloadServerData();
    }

    private string GetReportStatus(ReportStatus reportStatus)
    {
        switch (reportStatus)
        {
            case ReportStatus.Pending:
                return Resource.Pending;

            case ReportStatus.InProgress:
                return Resource.In_Progress;

            case ReportStatus.Completed:
                return Resource.Completed;

            case ReportStatus.Failed:
                return Resource.Failed;

            default:
                return Resource.All;
        }
    }

    private async Task<TableData<ReportItem>> ServerReload(TableState state)
    {
        GetReportsQuery.SearchText = SearchString;

        GetReportsQuery.SelectedReportStatus = SelectedReportStatus;

        GetReportsQuery.PageNumber = state.Page + 1;

        GetReportsQuery.RowsPerPage = state.PageSize;

        GetReportsQuery.SortBy = state.SortDirection == SortDirection.None
            ? string.Empty
            : $"{state.SortLabel} {state.SortDirection}";

        var httpResponseWrapper = await ReportsClient.GetReports(GetReportsQuery);

        var tableData = new TableData<ReportItem>();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ReportsResponse>;
            if (successResult != null)
                ReportsResponse = successResult.Result;

            tableData = new TableData<ReportItem>()
            { TotalItems = ReportsResponse.Reports.TotalRows, Items = ReportsResponse.Reports.Items };
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }

        return tableData;
    }

    private async Task StartHubConnection()
    {
        var subDomain = NavigationManager.GetSubDomain();

        HubConnection = new HubConnectionBuilder()
            .WithUrl($"{ApiUrlProvider.BaseHubUrl}/Hubs/DataExportHub?X-Tenant={subDomain}&Accept-Language={CultureInfo.CurrentCulture}",
                options =>
                {
                    //options.Headers.Add("X-Tenant", subDomain); //Doesn't Work
                    //options.Headers.Add("Accept-Language", culture); //Doesn't Work
                    options.AccessTokenProvider = () => AccessTokenProvider.TryGetAccessToken();
                }).Build();
        try
        {
            await HubConnection.StartAsync();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task ViewReport(string id)
    {
        var httpResponseWrapper = await ReportsClient.GetReport(new GetReportForEditQuery { Id = id });

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<ReportForEdit>;

            if (successResult != null)
                ReportForEdit = successResult.Result;

            var parameters = new DialogParameters { ["ReportForEdit"] = ReportForEdit };

            var options = new DialogOptions()
            { CloseButton = true, MaxWidth = MaxWidth.ExtraExtraLarge, CloseOnEscapeKey = true, FullScreen = true };

            DialogService.Show<ViewReportDialog>(Resource.Report_Details, parameters, options);
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Private Methods
}