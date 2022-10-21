namespace D2W.WebPortal.Features.Reports.GetReports;

public class ReportItem : AuditableDto
{
    #region Public Properties

    public string ContentType { get; set; }
    public string FileName { get; set; }
    public string FileUri { get; set; }
    public string Id { get; set; }
    public string QueryString { get; set; }
    public ReportStatus Status { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; }

    #endregion Public Properties
}