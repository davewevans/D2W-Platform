namespace D2W.Application.Common.Interfaces.Services;

public interface IReportingService
{
    #region Public Methods

    Task<ExportApplicantsResponse> ExportApplicantsAsPdfImmediate(ApplicantsResponse applicantsResponse, string host, string issuerName);

    Task InitiateApplicantsReport(ExportApplicantsQuery request, Guid reportId, string userNameIdentifier, Guid? tenantId = null);

    Task<FileMetaData> ExportApplicantsAsPdfInBackground(ExportApplicantsQuery request, Guid reportId, string host, string userName, string userNameIdentifier, Guid? tenantId = null);

    #endregion Public Methods
}