namespace D2W.Application.UseCases.Reports;

public class ReportUseCase : IReportUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;

    #endregion Private Fields

    #region Public Constructors

    public ReportUseCase(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<ReportForEdit>> GetReport(GetReportForEditQuery request)
    {
        if (!Guid.TryParse(request.Id, out var reportId))
            return Envelope<ReportForEdit>.Result.BadRequest(Resource.Invalid_report_Id);

        var report = await _dbContext.Reports.Where(a => a.Id == reportId).FirstOrDefaultAsync();

        if (report == null)
            return Envelope<ReportForEdit>.Result.NotFound(Resource.Unable_to_load_report);

        var reportForEdit = ReportForEdit.MapFromEntity(report);

        return Envelope<ReportForEdit>.Result.Ok(reportForEdit);
    }

    public async Task<Envelope<ReportsResponse>> GetReports(GetReportsQuery request)
    {
        var query = _dbContext.Reports.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(q => q.Title.Contains(request.SearchText) ||
                                     q.FileName.Contains(request.SearchText) ||
                                     q.FileUri.Contains(request.SearchText) ||
                                     q.QueryString.Contains(request.SearchText));

        query = query.Where(q => q.Status == (int)request.SelectedReportStatus || request.SelectedReportStatus == null || request.SelectedReportStatus == 0);

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderByDescending(a => a.CreatedOn);

        var reportItems = await query.Select(q => ReportItem.MapFromEntity(q)).AsNoTracking().ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var reportsResponse = new ReportsResponse
        {
            Reports = reportItems
        };

        return Envelope<ReportsResponse>.Result.Ok(reportsResponse);
    }

    #endregion Public Methods
}