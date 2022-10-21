namespace D2W.Infrastructure.Services;

public class TenantResolver : ITenantResolver
{
    #region Private Fields

    private Guid? _tenantId;
    private string _tenantName;

    #endregion Private Fields

    #region Public Properties

    public TenantMode TenantMode { get; set; }
    public bool IsHost { get; set; }

    #endregion Public Properties

    #region Public Methods

    public Guid? GetTenantId()
    {
        return _tenantId;
    }

    public string GetTenantName()
    {
        return _tenantName;
    }

    public void SetTenantId(Guid? tenantId)
    {
        _tenantId = tenantId;
    }

    public void SetTenantName(string tenantName)
    {
        _tenantName = tenantName;
    }

    #endregion Public Methods
}