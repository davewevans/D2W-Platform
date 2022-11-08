namespace D2W.WebAPI.Services.HubServices;

public class HubNotificationService : IHubNotificationService
{
    #region Private Fields

    private readonly IHubContext<DataExportHub> _hubContext;

    #endregion Private Fields

    #region Public Constructors

    public HubNotificationService(IHubContext<DataExportHub> hubContext)
    {
        _hubContext = hubContext;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task NotifyReportIssuer(string userNameIdentifier, FileMetaData fileMetaData, ReportStatus status)
    {
        await _hubContext.Clients.User(userNameIdentifier).SendAsync("NotifyReportIssuer", fileMetaData, status);
        //await _hubContext.Workrooms.All.SendAsync("NotifyReportSubscriber", $"Hi=>{userName}");
    }

    public async Task RefreshReportsViewer(string userNameIdentifier)
    {
        await _hubContext.Clients.User(userNameIdentifier).SendAsync("RefreshReportsViewer");
    }

    #endregion Public Methods
}