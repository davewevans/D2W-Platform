namespace D2W.Application.Common.Interfaces.UseCases.Reports
{
    public interface IReportUseCase
    {
        #region Public Methods

        Task<Envelope<ReportForEdit>> GetReport(GetReportForEditQuery request);

        Task<Envelope<ReportsResponse>> GetReports(GetReportsQuery request);

        #endregion Public Methods
    }
}