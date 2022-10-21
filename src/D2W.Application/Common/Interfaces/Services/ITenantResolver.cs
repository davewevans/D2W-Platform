namespace D2W.Application.Common.Interfaces.Services;

public interface ITenantResolver
{
    #region Public Properties

    TenantMode TenantMode { get; set; }
    bool IsHost { get; set; }

    #endregion Public Properties

    #region Public Methods

    Guid? GetTenantId();

    void SetTenantId(Guid? tenantId);

    string GetTenantName();

    void SetTenantName(string tenantName);

    #endregion Public Methods
}