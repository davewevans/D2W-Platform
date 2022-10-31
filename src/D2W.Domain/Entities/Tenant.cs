namespace D2W.Domain.Entities;

public class Tenant : IAuditable
{
    public Tenant()
    {
        TenantsWorkrooms = new List<TenantWorkroomModel>();
        TenantsClients = new List<TenantClientModel>();
    }

    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    // TODO add with migration
    public string StorageFileNamePrefix { get; set; }


    #endregion Public Properties

    #region Navigational Properties

    public ICollection<TenantWorkroomModel> TenantsWorkrooms { get; set; }
    public ICollection<TenantClientModel> TenantsClients { get; set; }

    #endregion
}