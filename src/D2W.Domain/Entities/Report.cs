namespace D2W.Domain.Entities;

public class Report : IAuditable, IMustHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string QueryString { get; set; }
    public string FileName { get; set; }
    public string FileUri { get; set; }
    public string ContentType { get; set; }
    public int Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid TenantId { get; set; }

    #endregion Public Properties
}