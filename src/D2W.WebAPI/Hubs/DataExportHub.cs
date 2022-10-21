namespace D2W.WebAPI.Hubs;

[Authorize]
public class DataExportHub : Hub
{
    #region Private Fields

    private readonly IBackgroundJobClient _backgroundJob;

    private readonly IReportingService _reportingService;

    private readonly ISignalRContextProvider _signalRContextProvider;

    #endregion Private Fields

    #region Public Constructors

    public DataExportHub(IBackgroundJobClient backgroundJob,
                         IReportingService reportingService,
                         ISignalRContextProvider signalRContextProvider)
    {
        _signalRContextProvider = signalRContextProvider;
        _backgroundJob = backgroundJob;
        _reportingService = reportingService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task ExportApplicantToPdf(ExportApplicantsQuery request)
    {
        var issuerName = _signalRContextProvider.GetUserName(Context);

        _signalRContextProvider.SetTenantIdViaTenantResolver(Context);

        var tenantId = _signalRContextProvider.GetTenantId(Context);

        var userNameIdentifier = _signalRContextProvider.GetUserNameIdentifier(Context);

        var httpRequest = Context.GetHttpContext()?.Request;

        if (httpRequest == null)
            throw new NullReferenceException(nameof(httpRequest));

        var host = _signalRContextProvider.GetHostName(Context);

        var reportId = Guid.NewGuid();

        //If TenantMode = MultiTenant && Hangfire = false, there is no need to pass the tenant id (as usual).

        #region WithoutHangfire

        //await _reportingService.InitiateApplicantsReport(request, reportId, userNameIdentifier);
        //await _reportingService.ExportApplicantsAsPdfInBackground(request, reportId, host, issuerName, userNameIdentifier);

        #endregion WithoutHangfire

        //If TenantMode = MultiTenant && Hangfire = true, the tenant id should be passed manually and must be set by tenant resolver (inside) the job method..

        #region WithHangfire

        //Add record to the Reports Table with pending status
        var pendingJob = _backgroundJob.Enqueue(() => _reportingService.InitiateApplicantsReport(request, reportId, userNameIdentifier, tenantId));

        //Add record to the Reports Table with in-progress status
        _backgroundJob.ContinueJobWith(pendingJob, () => _reportingService.ExportApplicantsAsPdfInBackground(request, reportId, host, issuerName, userNameIdentifier, tenantId));

        #endregion WithHangfire

        await Task.CompletedTask;
    }

    public override Task OnConnectedAsync()
    {
        if (Context.UserIdentifier != null)
        {
            var name = _signalRContextProvider.GetUserName(Context);
            Groups.AddToGroupAsync(Context.ConnectionId, name);
        }

        return base.OnConnectedAsync();
    }

    #endregion Public Methods
}