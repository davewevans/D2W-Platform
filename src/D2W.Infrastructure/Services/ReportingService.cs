namespace D2W.Infrastructure.Services;

public class ReportingService : IReportingService
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly IHtmlReportBuilder _htmlReportBuilder;
    private readonly IHubNotificationService _hubNotificationService;
    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public ReportingService(IHtmlReportBuilder htmlReportBuilder,
                            IApplicationDbContext dbContext,
                            ITenantResolver tenantResolver,
                            IHubNotificationService hubNotificationService)
    {
        _htmlReportBuilder = htmlReportBuilder;
        _dbContext = dbContext;
        _tenantResolver = tenantResolver;
        _hubNotificationService = hubNotificationService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<ExportApplicantsResponse> ExportApplicantsAsPdfImmediate(ApplicantsResponse applicantsResponse,
                                                                               string host,
                                                                               string issuerName)
    {
        var fileMetaData = await _htmlReportBuilder.GenerateApplicantsPdfFromHtml("HTMLTemplates/Applicants.html",
            host,
            "RequestedReports",
            "Applicants",
            issuerName,
            applicantsResponse.Applicants.Items);

        var response = new ExportApplicantsResponse
        {
            FileName = fileMetaData.FileName,
            FileUrl = fileMetaData.FileUri,
            ContentType = fileMetaData.ContentType
        };

        return response;
    }

    public async Task<FileMetaData> ExportApplicantsAsPdfInBackground(ExportApplicantsQuery request,
                                                                      Guid reportId,
                                                                      string host,
                                                                      string userName,
                                                                      string userNameIdentifier,
                                                                      Guid? tenantId)
    {
        SetTenantIdIfNotNull(tenantId);

        FileMetaData fileMetaData = new();

        try
        {
            await UpdateStatusAndNotify(reportId, ReportStatus.InProgress, userNameIdentifier, fileMetaData);

            var applicantsResponse = await GetApplicants(new GetApplicantsQuery
            {
                SearchText = request.SearchText,
                SortBy = request.SortBy
            });

            fileMetaData = await _htmlReportBuilder.GenerateApplicantsPdfFromHtml("HTMLTemplates/Applicants.html",
                                                                                  host,
                                                                                  "RequestedReports",
                                                                                  "Applicants",
                                                                                  userName,
                                                                                  applicantsResponse.Applicants.Items);

            await UpdateStatusAndNotify(reportId, ReportStatus.Completed, userNameIdentifier, fileMetaData);
        }
        catch
        {
            await UpdateStatusAndNotify(reportId, ReportStatus.Failed, userNameIdentifier, fileMetaData);
        }

        return fileMetaData;
    }

    public async Task InitiateApplicantsReport(ExportApplicantsQuery request,
                                               Guid reportId,
                                               string userNameIdentifier,
                                               Guid? tenantId)
    {
        await UpdateStatusAndNotify(reportId, ReportStatus.Pending, userNameIdentifier);

        SetTenantIdIfNotNull(tenantId);

        await SetInitialStatus(request, reportId);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<ApplicantsResponse> GetApplicants(GetApplicantsQuery request)
    {
        var query = _dbContext.Applicants.Include(a => a.References).Where(a => a.FirstName.Contains(request.SearchText)
                                                       || a.LastName.Contains(request.SearchText)
                                                       || request.SearchText == null);

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(a => a.FirstName).ThenBy(a => a.LastName);

        var applicantItems =
            await query.Select(q => ApplicantItem.MapFromEntity(q)).ToPagedListAsync(request.PageNumber, -1);

        var applicantsResponse = new ApplicantsResponse
        {
            Applicants = applicantItems
        };

        return applicantsResponse;
    }

    private async Task SetInitialStatus(ExportApplicantsQuery request, Guid reportId)
    {
        _dbContext.Reports.Add(new Report
        {
            Id = reportId,
            Title = $"Applicants-{DateTimeProvider.GetUnixTimeMilliseconds()}",
            QueryString = $"SearchText:{request.SearchText ?? "All"}, SortBy:{request.SortBy}",
            Status = (int)ReportStatus.Pending
        });
        await _dbContext.SaveChangesAsync();
    }

    private void SetTenantIdIfNotNull(Guid? tenantId)
    {
        if (tenantId is not null)
        {
            _tenantResolver.SetTenantId(tenantId);
            _tenantResolver.TenantMode = TenantMode.MultiTenant;
        }
    }

    private async Task UpdateStatusAndNotify(Guid reportId,
                                             ReportStatus status,
                                             string userNameIdentifier,
                                             FileMetaData fileMetaData = null)
    {
        await Task.Delay(1000);

        var report = await _dbContext.Reports.Where(r => r.Id == reportId).FirstOrDefaultAsync();

        if (report != null)
        {
            report.Status = (int)status;
            report.ContentType = fileMetaData?.ContentType;
            report.FileName = fileMetaData?.FileName;
            report.FileUri = fileMetaData?.FileUri;
            await _dbContext.SaveChangesAsync();
        }

        await _hubNotificationService.NotifyReportIssuer(userNameIdentifier, fileMetaData, status);
        await _hubNotificationService.RefreshReportsViewer(userNameIdentifier);
    }

    #endregion Private Methods
}