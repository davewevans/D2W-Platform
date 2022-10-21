namespace D2W.WebPortal.Consumers;

public class ReportsClient : IReportsClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public ReportsClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> GetReport(GetReportForEditQuery request)
    {
        return await _httpService.Post<GetReportForEditQuery, ReportForEdit>("reports/GetReport", request);
    }

    public async Task<HttpResponseWrapper<object>> GetReports(GetReportsQuery request)
    {
        return await _httpService.Post<GetReportsQuery, ReportsResponse>("reports/GetReports", request);
    }

    #endregion Public Methods
}