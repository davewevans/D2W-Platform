namespace D2W.Domain.Entities.POC;

public class Reference : IAuditable
{
    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string JobTitle { get; set; }
    public string Phone { get; set; }
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}