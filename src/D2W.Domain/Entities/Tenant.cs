namespace D2W.Domain.Entities;

public class Tenant : IAuditable
{
    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    // TODO add with migration
    public string StorageFileNamePrefix { get; set; }

    #endregion Public Properties
}