namespace D2W.Application.Common.Interfaces.Services.HubServices;

public interface IHubNotificationService
{
    #region Public Methods

    Task NotifyReportIssuer(string userNameIdentifier, FileMetaData fileMetaData, ReportStatus status);

    Task RefreshReportsViewer(string userNameIdentifier);

    #endregion Public Methods
}